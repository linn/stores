namespace Linn.Stores.Domain.LinnApps.Consignments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Linn.Common.Domain.Exceptions;
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

        private readonly IPrintService printService;

        private readonly IRepository<PrinterMapping, int> printerMappingRepository;

        private readonly IRepository<Consignment, int> consignmentRepository;

        private readonly IRepository<ExportBook, int> exportBookRepository;

        private readonly IRepository<Invoice, int> invoiceRepository;

        public ConsignmentService(
            IRepository<Consignment, int> consignmentRepository,
            IRepository<ExportBook, int> exportBookRepository,
            IConsignmentProxyService consignmentProxyService,
            IPrintService printService,
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
            this.printService = printService;
            this.printerMappingRepository = printerMappingRepository;
            this.consignmentRepository = consignmentRepository;
            this.exportBookRepository = exportBookRepository;
            this.invoiceRepository = invoiceRepository;
        }

        public async Task CloseConsignment(Consignment consignment, int closedById)
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

            await this.PrintDocuments(consignment, closedById);
        }

        public async Task<ProcessResult> PrintConsignmentDocuments(int consignmentId, int userNumber)
        {
            var printerName = this.GetPrinter(userNumber);
            var printerUri = this.GetPrinterUri(userNumber);
            var consignment = this.consignmentRepository.FindById(consignmentId);

            await this.PrintDocuments(consignment, userNumber);

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

        public ProcessResult SaveConsignmentDocuments(int consignmentId)
        {
            var consignment = this.consignmentRepository.FindById(consignmentId);

            foreach (var consignmentInvoice in consignment.Invoices)
            {
                this.printInvoiceDispatcher.SaveInvoice(
                    consignmentInvoice.DocumentNumber,
                    consignmentInvoice.DocumentType,
                    "CUSTOMER MASTER",
                    "Y",
                    $"Invoice {consignmentInvoice.DocumentNumber}.pdf");
            }

            this.printConsignmentNoteDispatcher.SaveConsignmentNote(
                consignment.ConsignmentId,
                $"Packing List {consignment.ConsignmentId}.pdf");

            return new ProcessResult(true, $"Documents saved for consignment {consignmentId}");
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

        private async Task PrintDocuments(Consignment consignment, int userNumber)
        {
            var printerUri = this.GetPrinterUri(userNumber);
            var printerName = this.GetPrinter(userNumber);

            var updatedConsignment = this.consignmentRepository.FindById(consignment.ConsignmentId);
            var numberOfCopies = consignment.Address.Country.NumberOfCopiesOfDispatchDocuments ?? 1;

            if (consignment.Address.Country.CountryCode != "GB")
            {
                this.PrintConsignmentNote(consignment, numberOfCopies, printerName);
            }

            await this.PrintInvoices(
                updatedConsignment,
                consignment.Address.Country.CountryCode,
                numberOfCopies,
                printerName,
                printerUri);

            this.MaybePrintExportBook(consignment, printerName, printerUri);
        }

        private async Task PrintInvoices(
            Consignment updatedConsignment,
            string countryCode,
            int numberOfCopies,
            string printerName,
            string printerUri)
        {
            foreach (var consignmentInvoice in updatedConsignment.Invoices)
            {
                for (var i = 1; i <= numberOfCopies; i++)
                {
                    if (countryCode != "GB")
                    {
                        try
                        {
                            // new temporary proxy print service
                            await this.printService.PrintDocument(
                                printerUri,
                                consignmentInvoice.DocumentType,
                                consignmentInvoice.DocumentNumber,
                                false,
                                true);
                        }
                        catch (DomainException exception)
                        {
                            // todo log and continue
                        }
                    }

                    try
                    {
                        // new temporary proxy print service
                        await this.printService.PrintDocument(
                            printerUri,
                            consignmentInvoice.DocumentType,
                            consignmentInvoice.DocumentNumber,
                            false,
                            false);
                    }
                    catch (DomainException exception)
                    {
                        // todo log and continue
                    }
                }
            }
        }

        private void PrintConsignmentNote(Consignment consignment, int numberOfCopies, string printerName)
        {
            for (var i = 1; i <= numberOfCopies; i++)
            {
                this.printConsignmentNoteDispatcher.PrintConsignmentNote(consignment.ConsignmentId, printerName);
            }
        }

        private void MaybePrintExportBook(Consignment consignment, string printerName, string printerUri)
        {
            var exportBooks =
                this.exportBookRepository.FilterBy(a => a.ConsignmentId == consignment.ConsignmentId);

            foreach (var exportBook in exportBooks)
            {
                try
                {
                    // new temporary proxy print service
                    this.printService.PrintDocument(printerUri, "E", exportBook.ExportId, true, true);
                }
                catch (DomainException exception)
                {
                    // todo log and continue
                }
            }
        }

        private string GetPrinter(int userNumber)
        {
            var printer = this.printerMappingRepository.FindBy(
                a => a.UserNumber == userNumber && a.PrinterGroup == "DISPATCH-INVOICE");

            if (!string.IsNullOrEmpty(printer?.PrinterName))
            {
                return printer.PrinterName;
            }

            printer = this.printerMappingRepository.FindBy(
                a => a.DefaultForGroup == "Y" && a.PrinterGroup == "DISPATCH-INVOICE");

            return printer?.PrinterName;
        }

        private string GetPrinterUri(int userNumber)
        {
            var printer = this.printerMappingRepository.FindBy(
                a => a.UserNumber == userNumber && a.PrinterGroup == "DISPATCH-INVOICE");

            if (!string.IsNullOrEmpty(printer?.PrinterUri))
            {
                return printer.PrinterUri;
            }

            printer = this.printerMappingRepository.FindBy(
                a => a.DefaultForGroup == "Y" && a.PrinterGroup == "DISPATCH-INVOICE");

            return printer?.PrinterUri;
        }
    }
}
