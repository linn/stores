namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps;

    public class StoragePlaceResponseProcessor : JsonResponseProcessor<StoragePlace>
    {
        public StoragePlaceResponseProcessor(IResourceBuilder<StoragePlace> resourceBuilder)
            : base(resourceBuilder, "storage-place", 1)
        {
        }
    }
}
