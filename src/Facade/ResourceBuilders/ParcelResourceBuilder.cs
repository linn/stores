namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
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
                SupplierName = parcel.SupplierName,
                SupplierCountry = parcel.SupplierCountry,
                DateCreated = parcel.DateCreated.ToString("o"),
                CarrierId = parcel.CarrierId,
                CarrierName = parcel.CarrierName,
                SupplierInvoiceNo = parcel.SupplierInvoiceNo,
                ConsignmentNo = parcel.ConsignmentNo,
                CartonCount = parcel.CartonCount,
                PalletCount = parcel.PalletCount,
                Weight = parcel.Weight,
                DateReceived = parcel.DateReceived.ToString("o"),
                CheckedById = parcel.CheckedById,
                CheckedByName = parcel.CheckedByName,
                Comments = parcel.Comments,
                ImportNoVax = parcel.ImportNoVax, //todo check if this should be int
                ImpbookId = parcel.ImpbookId
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

        private bool? ToNullableBool(string yesOrNoString)
        {
            if (yesOrNoString == null)
            {
                return null;
            }

            return yesOrNoString == "Y";
        }
    }
}
