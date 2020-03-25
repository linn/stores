namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class ParcelsResourceBuilder : IResourceBuilder<IEnumerable<Parcel>>
    {
        private readonly ParcelResourceBuilder parcelResourceBuilder = new ParcelResourceBuilder();

        public IEnumerable<ParcelResource> Build(IEnumerable<Parcel> parcels)
        {
            return parcels
                .OrderBy(b => b.ParcelNumber)
                .Select(a => this.parcelResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<Parcel>>.Build(IEnumerable<Parcel> parts) => this.Build(parts);

        public string GetLocation(IEnumerable<Parcel> parts)
        {
            throw new NotImplementedException();
        }
    }
}
