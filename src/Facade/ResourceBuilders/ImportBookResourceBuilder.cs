namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.ImportBooks;

    public class ImportBookResourceBuilder : IResourceBuilder<ImportBook>
    {
        private readonly ImportBookPostEntriesResourceBuilder postEntriesResourceBuilder =
            new ImportBookPostEntriesResourceBuilder();

        private readonly ImportBookOrderDetailsResourceBuilder orderDetailsResourceBuilder =
            new ImportBookOrderDetailsResourceBuilder();

        private readonly ImportBookInvoiceDetailsResourceBuilder invoiceDetailsResourceBuilder =
            new ImportBookInvoiceDetailsResourceBuilder();

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
                TransportId = model.TransportId,
                TransportBillNumber = model.TransportBillNumber,
                TransactionId = model.TransactionId,
                DeliveryTermCode = model.DeliveryTermCode,
                ArrivalPort = model.ArrivalPort,
                ArrivalDate = model.ArrivalDate?.ToString("o"),
                TotalImportValue = model.TotalImportValue,
                Weight = model.Weight,
                CustomsEntryCode = model.CustomsEntryCode,
                CustomsEntryCodeDate = model.CustomsEntryCodeDate?.ToString("o"),
                LinnDuty = model.LinnDuty,
                LinnVat = model.LinnVat,
                DateCancelled = model.DateCancelled?.ToString("o"),
                CancelledBy = model.CancelledBy,
                CancelledReason = model.CancelledReason,
                NumCartons = model.NumCartons,
                NumPallets = model.NumPallets,
                Comments = model.Comments,
                CreatedBy = model.CreatedBy,
                CustomsEntryCodePrefix = model.CustomsEntryCodePrefix,
                Pva = model.Pva,
                ExchangeRate = model.ExchangeRate.GetValueOrDefault(),
                ImportBookPostEntries = model.PostEntries != null ? this.postEntriesResourceBuilder.Build(model.PostEntries) : new List<ImportBookPostEntryResource>(),
                ImportBookOrderDetails = model.OrderDetails != null ? this.orderDetailsResourceBuilder.Build(model.OrderDetails) : new List<ImportBookOrderDetailResource>(),
                ImportBookInvoiceDetails = model.InvoiceDetails != null ? this.invoiceDetailsResourceBuilder.Build(model.InvoiceDetails) : new List<ImportBookInvoiceDetailResource>(),
                Links = this.BuildLinks(model).ToArray()
            };
        }

        public string GetLocation(ImportBook model)
        {
            return $"/logistics/import-books/{model.Id}";
        }

        object IResourceBuilder<ImportBook>.Build(ImportBook importBook) => this.Build(importBook);

        private IEnumerable<LinkResource> BuildLinks(ImportBook importBook)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(importBook) };
        }
    }
}
