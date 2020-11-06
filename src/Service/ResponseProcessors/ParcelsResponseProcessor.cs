namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps;

    public class ParcelsResponseProcessor : JsonResponseProcessor<IEnumerable<Parcel>>
    {
        public ParcelsResponseProcessor(IResourceBuilder<IEnumerable<Parcel>> resourceBuilder)
            : base(resourceBuilder, "parcels", 1)
        {
        }
    }
}
