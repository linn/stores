namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps;

    public class ParcelResponseProcessor : JsonResponseProcessor<Parcel>
    {
        public ParcelResponseProcessor(IResourceBuilder<Parcel> resourceBuilder)
            : base(resourceBuilder, "parcel", 1)
        {
        }
    }
}
