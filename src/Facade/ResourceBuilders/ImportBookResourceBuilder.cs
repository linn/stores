namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.Parts;

    public class ImportBookResourceBuilder : IResourceBuilder<ImportBook>
    {
        private readonly ImportBookOrderDetailResourceBuilder importBookOrderDetailResourceBuilder =
            new ImportBookOrderDetailResourceBuilder();

        private readonly ImportBookInvoiceDetailResourceBuilder importBookInvoiceDetailResourceBuilder =
            new ImportBookInvoiceDetailResourceBuilder();

        private readonly ImportBookPostEntryResourceBuilder importBookPostEntryResourceBuilder =
            new ImportBookPostEntryResourceBuilder();

        public ImportBookResource Build(ImportBook model)
        {
            return new ImportBookResource
                       {
                           Id = model.Id,
                           DateCreated = model.DateCreated.ToString("o"),
                           ParcelNumber = model.ParcelNumber,
                           SupplierId = model.SupplierId,
                           ForeignCurrency = model.ForeignCurrency,
                           Currency = model.Currency,
                           CarrierId = model.CarrierId,
                           OldArrivalPort = model.OldArrivalPort,
                           FlightNumber = model.FlightNumber,
                           TransportId = model.TransportId,
                           TransportBillNumber = model.TransportBillNumber,
                           TransactionId = model.TransactionId,
                           DeliveryTermCode = model.DeliveryTermCode,
                           ArrivalPort = model.ArrivalPort,
                           LineVatTotal = model.LineVatTotal,
                           Hwb = model.Hwb,
                           SupplierCostCurrency = model.SupplierCostCurrency,
                           TransNature = model.TransNature,
                           ArrivalDate = model.ArrivalDate?.ToString("o"),
                           FreightCharges = model.FreightCharges,
                           HandlingCharge = model.HandlingCharge,
                           ClearanceCharge = model.ClearanceCharge,
                           Cartage = model.Cartage,
                           Duty = model.Duty,
                           Vat = model.Vat,
                           Misc = model.Misc,
                           CarriersInvTotal = model.CarriersInvTotal,
                           CarriersVatTotal = model.CarriersVatTotal,
                           TotalImportValue = model.TotalImportValue,
                           Pieces = model.Pieces,
                           Weight = model.Weight,
                           CustomsEntryCode = model.CustomsEntryCode,
                           CustomsEntryCodeDate = model.CustomsEntryCodeDate?.ToString("o"),
                           LinnDuty = model.LinnDuty,
                           LinnVat = model.LinnVat,
                           IprCpcNumber = model.IprCpcNumber,
                           EecgNumber = model.EecgNumber,
                           DateCancelled = model.DateCancelled?.ToString("o"),
                           CancelledBy = model.CancelledBy,
                           CancelledReason = model.CancelledReason,
                           CarrierInvNumber = model.CarrierInvNumber,
                           CarrierInvDate = model.CarrierInvDate?.ToString("o"),
                           CountryOfOrigin = model.CountryOfOrigin,
                           FcName = model.FcName,
                           VaxRef = model.VaxRef,
                           Storage = model.Storage,
                           NumCartons = model.NumCartons,
                           NumPallets = model.NumPallets,
                           Comments = model.Comments,
                           ExchangeRate = model.ExchangeRate,
                           ExchangeCurrency = model.ExchangeCurrency,
                           BaseCurrency = model.BaseCurrency,
                           PeriodNumber = model.PeriodNumber,
                           CreatedBy = model.CreatedBy,
                           PortCode = model.PortCode,
                           CustomsEntryCodePrefix = model.CustomsEntryCodePrefix,
                           ImportBookOrderDetail =
                               model.OrderDetail != null
                                   ? this.importBookOrderDetailResourceBuilder.Build(model.OrderDetail)
                                   : null,
                           ImportBookPostEntry =
                               model.PostEntry != null
                                   ? this.importBookPostEntryResourceBuilder.Build(model.PostEntry)
                                   : null,
                           ImportBookInvoiceDetail =
                               model.InvoiceDetail != null
                                   ? this.importBookInvoiceDetailResourceBuilder.Build(model.InvoiceDetail)
                                   : null,
                           Links = this.BuildLinks(model).ToArray()
                       };
        }

        public string GetLocation(ImportBook model)
        {
            return $"/inventory/import-books/{model.Id}";
        }

        object IResourceBuilder<ImportBook>.Build(ImportBook importBook) => this.Build(importBook);

        private IEnumerable<LinkResource> BuildLinks(ImportBook importBook)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(importBook) };
        }
    }
}