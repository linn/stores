namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class ParcelResourceBuilder : IResourceBuilder<Parcel>
    {
        public ParcelResource Build(Parcel parcel)
        {
            return new ParcelResource
            {
                ParcelNumber = parcel.ParcelNumber,
                SupplierId = parcel.SupplierId,
                DateCreated = parcel.DateCreated.ToString("o"),
                CarrierId = parcel.CarrierId,
                SupplierInvoiceNo = parcel.SupplierInvoiceNo,
                ConsignmentNo = parcel.ConsignmentNo,
                CartonCount = parcel.CartonCount,
                PalletCount = parcel.PalletCount,
                Weight = parcel.Weight,
                DateReceived = parcel.DateReceived?.ToString("o"),
                CheckedById = parcel.CheckedById,
                Comments = parcel.Comments,
                DateCancelled = parcel.DateCancelled?.ToString("o"),
                CancellationReason = parcel.CancellationReason,
                CancelledBy = parcel.CancelledBy,
                Links = this.BuildLinks(parcel).ToArray()
            };
        }

        public string GetLocation(Parcel p)
        {
            return $"/logistics/parcels/{p.ParcelNumber}";
        }

        object IResourceBuilder<Parcel>.Build(Parcel parcel) => this.Build(parcel);

        private IEnumerable<LinkResource> BuildLinks(Parcel parcel)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(parcel) };
        }
    }
}
