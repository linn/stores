namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.Parts;

    public class ImportBookFacadeService : FacadeService<ImportBook, int, ImportBookResource, ImportBookResource>
    {
        private readonly IImportBookService importBookService;

        public ImportBookFacadeService(
            IRepository<ImportBook, int> repository,
            ITransactionManager transactionManager,
            IImportBookService importBookService)
            : base(repository, transactionManager)
        {
            this.importBookService = importBookService;
        }

        protected override ImportBook CreateFromResource(ImportBookResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(ImportBook entity, ImportBookResource updateResource)
        {
            var newImportBook = new ImportBook
                                    {
                                        ParcelNumber = updateResource.ParcelNumber,
                                        SupplierId = updateResource.SupplierId,
                                        ForeignCurrency = updateResource.ForeignCurrency,
                                        Currency = updateResource.Currency,
                                        CarrierId = updateResource.CarrierId,
                                        OldArrivalPort = updateResource.OldArrivalPort,
                                        FlightNumber = updateResource.FlightNumber,
                                        TransportId = updateResource.TransportId,
                                        TransportBillNumber = updateResource.TransportBillNumber,
                                        TransactionId = updateResource.TransactionId,
                                        DeliveryTermCode = updateResource.DeliveryTermCode,
                                        ArrivalPort = updateResource.ArrivalPort,
                                        LineVatTotal = updateResource.LineVatTotal,
                                        Hwb = updateResource.Hwb,
                                        SupplierCostCurrency = updateResource.SupplierCostCurrency,
                                        TransNature = updateResource.TransNature,
                                        ArrivalDate =
                                            string.IsNullOrWhiteSpace(updateResource.ArrivalDate)
                                                ? (DateTime?)null
                                                : DateTime.Parse(updateResource.ArrivalDate),
                                        FreightCharges = updateResource.FreightCharges,
                                        HandlingCharge = updateResource.HandlingCharge,
                                        ClearanceCharge = updateResource.ClearanceCharge,
                                        Cartage = updateResource.Cartage,
                                        Duty = updateResource.Duty,
                                        Vat = updateResource.Vat,
                                        Misc = updateResource.Misc,
                                        CarriersInvTotal = updateResource.CarriersInvTotal,
                                        CarriersVatTotal = updateResource.CarriersVatTotal,
                                        TotalImportValue = updateResource.TotalImportValue,
                                        Pieces = updateResource.Pieces,
                                        Weight = updateResource.Weight,
                                        CustomsEntryCode = updateResource.CustomsEntryCode,
                                        CustomsEntryCodeDate =
                                            string.IsNullOrWhiteSpace(updateResource.CustomsEntryCodeDate)
                                                ? (DateTime?)null
                                                : DateTime.Parse(updateResource.CustomsEntryCodeDate),
                                        LinnDuty = updateResource.LinnDuty,
                                        LinnVat = updateResource.LinnVat,
                                        IprCpcNumber = updateResource.IprCpcNumber,
                                        EecgNumber = updateResource.EecgNumber,
                                        DateCancelled =
                                            string.IsNullOrWhiteSpace(updateResource.DateCancelled)
                                                ? (DateTime?)null
                                                : DateTime.Parse(updateResource.DateCancelled),
                                        CancelledBy = updateResource.CancelledBy,
                                        CancelledReason = updateResource.CancelledReason,
                                        CarrierInvNumber = updateResource.CarrierInvNumber,
                                        CarrierInvDate =
                                            string.IsNullOrWhiteSpace(updateResource.CarrierInvDate)
                                                ? (DateTime?)null
                                                : DateTime.Parse(updateResource.CarrierInvDate),
                                        CountryOfOrigin = updateResource.CountryOfOrigin,
                                        FcName = updateResource.FcName,
                                        VaxRef = updateResource.VaxRef,
                                        Storage = updateResource.Storage,
                                        NumCartons = updateResource.NumCartons,
                                        NumPallets = updateResource.NumPallets,
                                        Comments = updateResource.Comments,
                                        ExchangeRate = updateResource.ExchangeRate,
                                        ExchangeCurrency = updateResource.ExchangeCurrency,
                                        BaseCurrency = updateResource.BaseCurrency,
                                        PeriodNumber = updateResource.PeriodNumber,
                                        CreatedBy = updateResource.CreatedBy,
                                        PortCode = updateResource.PortCode,
                                        CustomsEntryCodePrefix = updateResource.CustomsEntryCodePrefix
                                    };

            var invoiceDetails = new List<ImportBookInvoiceDetail>();
            foreach (var detail in updateResource.ImportBookInvoiceDetails)
            {
                invoiceDetails.Add(
                    new ImportBookInvoiceDetail
                        {
                            ImportBookId = updateResource.Id,
                            LineNumber = detail.LineNumber,
                            InvoiceNumber = detail.InvoiceNumber,
                            InvoiceValue = detail.InvoiceValue
                        });
            }

            newImportBook.InvoiceDetails = invoiceDetails;

            var orderDetails = new List<ImportBookOrderDetail>();
            foreach (var detail in updateResource.ImportBookOrderDetails)
            {
                orderDetails.Add(
                    new ImportBookOrderDetail
                        {
                            ImportBookId = updateResource.Id,
                            LineNumber = detail.LineNumber,
                            OrderNumber = detail.OrderNumber,
                            RsnNumber = detail.RsnNumber,
                            OrderDescription = detail.OrderDescription,
                            Qty = detail.Qty,
                            DutyValue = detail.DutyValue,
                            FreightValue = detail.FreightValue,
                            VatValue = detail.VatValue,
                            OrderValue = detail.OrderValue,
                            Weight = detail.Weight,
                            LoanNumber = detail.LoanNumber,
                            LineType = detail.LineType,
                            CpcNumber = detail.CpcNumber,
                            TariffCode = detail.TariffCode,
                            InsNumber = detail.InsNumber,
                            VatRate = detail.VatRate
                        });
            }

            newImportBook.OrderDetails = orderDetails;

            var postEntries = new List<ImportBookPostEntry>();
            foreach (var entry in updateResource.ImportBookPostEntries)
            {
                postEntries.Add(
                    new ImportBookPostEntry
                        {
                            ImportBookId = updateResource.Id,
                            LineNumber = entry.LineNumber,
                            EntryCodePrefix = entry.EntryCodePrefix,
                            EntryCode = entry.EntryCode,
                            EntryDate = string.IsNullOrWhiteSpace(entry.EntryDate)
                                            ? (DateTime?)null
                                            : DateTime.Parse(entry.EntryDate),
                            Reference = entry.Reference,
                            Duty = entry.Duty,
                            Vat = entry.Vat
                        });
            }

            newImportBook.PostEntries = postEntries;

            this.importBookService.Update(from: entity, to: newImportBook);
        }

        protected override Expression<Func<ImportBook, bool>> SearchExpression(string searchTerm)
        {
            return imps => imps.Id.ToString().Contains(searchTerm);
        }
    }
}
