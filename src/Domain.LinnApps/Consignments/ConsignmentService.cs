namespace Linn.Stores.Domain.LinnApps.Consignments
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Domain.Exceptions;
    using Linn.Common.Logging;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Consignments.Models;
    using Linn.Stores.Domain.LinnApps.Dispatchers;
    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.ExportBooks;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Models;

    public class ConsignmentService : IConsignmentService
    {
        private readonly IConsignmentProxyService consignmentProxyService;

        private readonly IInvoicingPack invoicingPack;

        private readonly IExportBookPack exportBookPack;

        private readonly IPrintInvoiceDispatcher printInvoiceDispatcher;

        private readonly IPrintConsignmentNoteDispatcher printConsignmentNoteDispatcher;

        private readonly ILog log; 

        private readonly IRepository<PrinterMapping, int> printerMappingRepository;

        private readonly IRepository<Consignment, int> consignmentRepository;

        private readonly IRepository<ExportBook, int> exportBookRepository;

        private readonly IRepository<Invoice, int> invoiceRepository;

        public ConsignmentService(
            IRepository<Consignment, int> consignmentRepository,
            IRepository<ExportBook, int> exportBookRepository,
            IConsignmentProxyService consignmentProxyService,
            ILog log,
            IInvoicingPack invoicingPack,
            IExportBookPack exportBookPack,
            IPrintInvoiceDispatcher printInvoiceDispatcher,
            IPrintConsignmentNoteDispatcher printConsignmentNoteDispatcher,
            IRepository<PrinterMapping, int> printerMappingRepository,
            IRepository<Invoice, int> invoiceRepository)
        {
            this.consignmentProxyService = consignmentProxyService;
            this.invoicingPack = invoicingPack;
            this.exportBookPack = exportBookPack;
            this.printInvoiceDispatcher = printInvoiceDispatcher;
            this.printConsignmentNoteDispatcher = printConsignmentNoteDispatcher;
            this.log = log;
            this.printerMappingRepository = printerMappingRepository;
            this.consignmentRepository = consignmentRepository;
            this.exportBookRepository = exportBookRepository;
            this.invoiceRepository = invoiceRepository;
        }

        public void CloseConsignment(Consignment consignment, int closedById)
        {
            if (closedById <= 0)
            {
                throw new ConsignmentCloseException(
                    $"Could not close consignment {consignment.ConsignmentId}. A valid closed by id must be supplied");
            }

            if (consignment.Status != "L")
            {
                throw new ConsignmentCloseException(
                    $"Could not close consignment {consignment.ConsignmentId} is already closed");
            }

            var canClose = this.consignmentProxyService.CanCloseAllocation(consignment.ConsignmentId);

            if (!canClose.Success)
            {
                throw new ConsignmentCloseException(
                    $"Cannot close consignment {consignment.ConsignmentId}. {canClose.Message}");
            }

            var invoiceResult = this.invoicingPack.InvoiceConsignment(consignment.ConsignmentId, closedById);

            if (!invoiceResult.Success)
            {
                throw new ConsignmentCloseException(
                    $"Failed to invoice consignment {consignment.ConsignmentId}. {invoiceResult.Message}");
            }

            if (consignment.HubId.HasValue)
            {
                this.exportBookPack.MakeExportBookFromConsignment(consignment.ConsignmentId);
            }

            this.PrintDocuments(consignment);
        }

        public ProcessResult PrintConsignmentDocuments(int consignmentId)
        {
            var consignment = this.consignmentRepository.FindById(consignmentId);
            
            this.PrintDocuments(consignment);

            return new ProcessResult(true, $"Documents printed for consignment {consignmentId}");
        }

        public PackingList GetPackingList(int consignmentId)
        {
            var consignment = this.consignmentRepository.FindById(consignmentId);
            if (consignment == null)
            {
                throw new NotFoundException($"Could not find consignment {consignmentId}");
            }

            var result = new PackingList
                             {
                                 SenderAddress = new Address
                                                     {
                                                         Addressee = "Linn Products Ltd",
                                                         Line1 = "Glasgow Road",
                                                         Line2 = "Eaglesham",
                                                         Line3 = "Glasgow",
                                                         CountryCode = "GB",
                                                         PostCode = "G76 0EQ",
                                                         Country = new Country { DisplayName = "United Kingdom", CountryCode = "GB" }
                                                     },
                                 DeliveryAddress = consignment.Address,
                                 DespatchDate = consignment.DateClosed,
                                 ConsignmentId = consignmentId,
                                 NumberOfPallets = consignment.Pallets?.Count ?? 0
                             };

            var itemsNotOnPallets = consignment.Items?.Where(this.CanBePutOnPallet).ToList();
            result.Items = itemsNotOnPallets?.Select(
                i => new PackingListItem
                         {
                             ContainerNumber = i.ContainerNumber,
                             ItemNumber = i.ItemNumber,
                             Volume = i.Height / 100m * i.Width / 100m * i.Depth / 100m,
                             Weight = i.Weight,
                             DisplayDimensions = $"{i.Height} x {i.Width} x {i.Depth}cm",
                             Description = this.PackingListDescription(i, consignment.Items)
                         }).ToList();
            result.NumberOfItemsNotOnPallets = itemsNotOnPallets?.Count ?? 0;

            result.Pallets = consignment.Pallets?.Select(
                p => new PackingListPallet
                         {
                             PalletNumber = p.PalletNumber,
                             Weight = p.Weight,
                             DisplayWeight = $"{p.Weight} Kgs",
                             DisplayDimensions = $"{p.Height} x {p.Width} x {p.Depth}cm",
                             Volume = p.Height / 100m * p.Width / 100m * p.Depth / 100m,
                             Items = consignment.Items.Where(a => a.PalletNumber == p.PalletNumber)
                                 .Select(b => new PackingListItem
                                                  {
                                                      ContainerNumber = b.ContainerNumber,
                                                      ItemNumber = b.ItemNumber,
                                                      Description = this.PackingListDescription(b, consignment.Items),
                                                      Volume = b.Height / 100m * b.Width / 100m * b.Depth / 100m,
                                                      Weight = b.Weight,
                                                      DisplayDimensions = $"{b.Height} x {b.Width} x {b.Depth}cm"
                                 }).ToList()
                         }).ToList();

            result.TotalVolume = decimal.Round((result.Pallets?.Sum(a => a.Volume) ?? 0) + (result.Items?.Sum(a => a.Volume) ?? 0), 3);
            result.TotalGrossWeight = (result.Pallets?.Sum(p => p.Weight) ?? 0) + (result.Items?.Sum(i => i.Weight) ?? 0);
            return result;
        }

        public IEnumerable<Consignment> GetByInvoiceNumber(int invoiceNumber)
        {
            var invoice = this.invoiceRepository.FindById(invoiceNumber);
            if (invoice == null)
            {
                return new List<Consignment>();
            }

            if (invoice.ConsignmentId == null)
            {
                return new List<Consignment>();
            }

            var consignment = this.consignmentRepository.FindById(invoice.ConsignmentId.Value);

            if (consignment == null)
            {
                return null;
            }

            return new List<Consignment>() { consignment };
        }

        private string PackingListDescription(ConsignmentItem selectedItem, IEnumerable<ConsignmentItem> items)
        {
            if (selectedItem.ItemType == "I" && !selectedItem.ContainerNumber.HasValue)
            {
                return $"{selectedItem.Quantity} {selectedItem.ItemDescription}";
            }

            var description = string.Empty;
            foreach (var item in items.Where(
                a => (a.ItemType == "I" || a.ItemType == "S")
                     && a.ContainerNumber == selectedItem.ContainerNumber))
            {
                var qty = item.MaybeHalfAPair == "Y" ? item.Quantity * 2 : item.Quantity;
                description += $"{qty} {item.ItemDescription}";
            }

            return string.IsNullOrEmpty(description) ? selectedItem.ItemDescription : description;
        }

        private bool CanBePutOnPallet(ConsignmentItem consignmentItem)
        {
            if (consignmentItem.PalletNumber.HasValue)
            {
                return false;
            }

            return (consignmentItem.ItemType == "I" && consignmentItem.ContainerNumber is null)
                   || consignmentItem.ItemType == "S" || consignmentItem.ItemType == "C";
        }

        private void PrintDocuments(Consignment consignment)
        {
            var updatedConsignment = this.consignmentRepository.FindById(consignment.ConsignmentId);
            var numberOfCopies = consignment.Address.Country.NumberOfCopiesOfDispatchDocuments ?? 1;

            if (consignment.Address.Country.CountryCode != "GB")
            {
                this.log.Info($"Printing Consignment note via msg");

                this.PrintConsignmentNote(consignment, numberOfCopies, "DISPATCH-INVOICE");
            }

            this.PrintInvoices(
                updatedConsignment,
                consignment.Address.Country.CountryCode,
                numberOfCopies,
                "DISPATCH-INVOICE");

            this.MaybePrintExportBook(consignment, "DISPATCH-INVOICE");
        }

        private void PrintInvoices(
            Consignment updatedConsignment,
            string countryCode,
            int numberOfCopies,
            string printerGroup)
        {
            foreach (var consignmentInvoice in updatedConsignment.Invoices)
            {
                for (var i = 1; i <= numberOfCopies; i++)
                {
                    if (countryCode != "GB")
                    {
                        try
                        {
                            this.log.Info($"Document with {consignmentInvoice.DocumentType} {consignmentInvoice.DocumentNumber} {countryCode} sent to {printerGroup}. Regular invoice.");

                            this.printInvoiceDispatcher.PrintInvoice(
                                printerGroup,
                                consignmentInvoice.DocumentType,
                                consignmentInvoice.DocumentNumber,
                                false,
                                true);
                        }
                        catch (PrintServiceException exception)
                        {
                            this.log.Error($"Printing failed for document {consignmentInvoice.DocumentNumber} to {printerGroup}");
                        }
                    }

                    try
                    {
                        this.log.Info(
                            $"Document with {consignmentInvoice.DocumentType} {consignmentInvoice.DocumentNumber} " +
                            $"{countryCode} sent to {printerGroup}. Delivery note with no prices.");

                        this.printInvoiceDispatcher.PrintInvoice(
                            printerGroup,
                            consignmentInvoice.DocumentType, 
                            consignmentInvoice.DocumentNumber, 
                            false,
                            false);
                    }
                    catch (PrintServiceException exception)
                    {
                        this.log.Error($"Printing failed for document {consignmentInvoice.DocumentNumber} to {printerGroup}. Delivery note with no prices.");
                    }
                }
            }
        }

        private void PrintConsignmentNote(Consignment consignment, int numberOfCopies, string printerGroup)
        {
            for (var i = 1; i <= numberOfCopies; i++)
            {
                this.printConsignmentNoteDispatcher.PrintConsignmentNote(consignment.ConsignmentId, printerGroup);
            }
        }

        private void MaybePrintExportBook(Consignment consignment, string printerGroup)
        {
            this.log.Info($"Search for Export Books. Consignment Id {consignment.ConsignmentId}");

            var exportBooks =
                this.exportBookRepository.FilterBy(a => a.ConsignmentId == consignment.ConsignmentId);

            this.log.Info($"Number of Export Books : {exportBooks.Count()}");

            foreach (var exportBook in exportBooks)
            {
                try
                {
                    this.log.Info(
                        $"Consignment with Id {exportBook.ConsignmentId} Export Id {exportBook.ExportId} " +
                        $"sent to {printerGroup}. Export Book.");

                    this.printInvoiceDispatcher.PrintInvoice(printerGroup, "E", exportBook.ExportId, false, true);
                }
                catch (PrintServiceException exception)
                {
                    this.log.Error($"Printing failed for document {consignment.ConsignmentId} Export Id {exportBook.ExportId} to {printerGroup}. Export Book.");
                }
            }
        }
    }
}
