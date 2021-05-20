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

        public ImportBookFacadeService(IRepository<ImportBook, int> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override ImportBook CreateFromResource(ImportBookResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(ImportBook entity, ImportBookResource updateResource)
        {
            entity.DateCreated = DateTime.Parse(updateResource.DateCreated);
            entity.ParcelNumber = updateResource.ParcelNumber;
            entity.SupplierId = updateResource.SupplierId;
            entity.ForeignCurrency = updateResource.ForeignCurrency;
            entity.Currency = updateResource.Currency;
            entity.CarrierId = updateResource.CarrierId;
            entity.OldArrivalPort = updateResource.OldArrivalPort;
            entity.FlightNumber = updateResource.FlightNumber;
            entity.TransportId = updateResource.TransportId;
            entity.TransportBillNumber = updateResource.TransportBillNumber;
            entity.TransactionId = updateResource.TransactionId;
            entity.DeliveryTermCode = updateResource.DeliveryTermCode;
            entity.ArrivalPort = updateResource.ArrivalPort;
            entity.LineVatTotal = updateResource.LineVatTotal;
            entity.Hwb = updateResource.Hwb;
            entity.SupplierCostCurrency = updateResource.SupplierCostCurrency;
            entity.TransNature = updateResource.TransNature;
            entity.ArrivalDate = string.IsNullOrWhiteSpace(updateResource.ArrivalDate)
                                     ? (DateTime?)null
                                     : DateTime.Parse(updateResource.ArrivalDate);
            entity.FreightCharges = updateResource.FreightCharges;
            entity.HandlingCharge = updateResource.HandlingCharge;
            entity.ClearanceCharge = updateResource.ClearanceCharge;
            entity.Cartage = updateResource.Cartage;
            entity.Duty = updateResource.Duty;
            entity.Vat = updateResource.Vat;
            entity.Misc = updateResource.Misc;
            entity.CarriersInvTotal = updateResource.CarriersInvTotal;
            entity.CarriersVatTotal = updateResource.CarriersVatTotal;
            entity.TotalImportValue = updateResource.TotalImportValue;
            entity.Pieces = updateResource.Pieces;
            entity.Weight = updateResource.Weight;
            entity.CustomsEntryCode = updateResource.CustomsEntryCode;
            entity.CustomsEntryCodeDate = string.IsNullOrWhiteSpace(updateResource.CustomsEntryCodeDate)
                                              ? (DateTime?)null
                                              : DateTime.Parse(updateResource.CustomsEntryCodeDate);
            entity.LinnDuty = updateResource.LinnDuty;
            entity.LinnVat = updateResource.LinnVat;
            entity.IprCpcNumber = updateResource.IprCpcNumber;
            entity.EecgNumber = updateResource.EecgNumber;
            entity.DateCancelled = string.IsNullOrWhiteSpace(updateResource.DateCancelled)
                                       ? (DateTime?)null
                                       : DateTime.Parse(updateResource.DateCancelled);
            entity.CancelledBy = updateResource.CancelledBy;
            entity.CancelledReason = updateResource.CancelledReason;
            entity.CarrierInvNumber = updateResource.CarrierInvNumber;
            entity.CarrierInvDate = string.IsNullOrWhiteSpace(updateResource.CarrierInvDate)
                                        ? (DateTime?)null
                                        : DateTime.Parse(updateResource.CarrierInvDate);
            entity.CountryOfOrigin = updateResource.CountryOfOrigin;
            entity.FcName = updateResource.FcName;
            entity.VaxRef = updateResource.VaxRef;
            entity.Storage = updateResource.Storage;
            entity.NumCartons = updateResource.NumCartons;
            entity.NumPallets = updateResource.NumPallets;
            entity.Comments = updateResource.Comments;
            entity.ExchangeRate = updateResource.ExchangeRate;
            entity.ExchangeCurrency = updateResource.ExchangeCurrency;
            entity.BaseCurrency = updateResource.BaseCurrency;
            entity.PeriodNumber = updateResource.PeriodNumber;
            entity.CreatedBy = updateResource.CreatedBy;
            entity.PortCode = updateResource.PortCode;
            entity.CustomsEntryCodePrefix = updateResource.CustomsEntryCodePrefix;

            var invoiceDetails = new List<ImportBookInvoiceDetail>();
            foreach (var detail in updateResource.ImportBookInvoiceDetails)
            {
                invoiceDetails.Add(new ImportBookInvoiceDetail
                                       {
                                           ImportBookId = updateResource.Id,
                                           LineNumber = detail.LineNumber,
                                           InvoiceNumber = detail.InvoiceNumber,
                                           InvoiceValue = detail.InvoiceValue
                                       });
            }

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


            this.importBookService.Update(entity, invoiceDetail, orderDetail, postEntry);

        }

    protected override Expression<Func<ImportBook, bool>> SearchExpression(string searchTerm)
        {
            return imps => imps.Id.ToString().Contains(searchTerm);
        }
    }
}