namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    public class StorageLocationsResponseProcessor : JsonResponseProcessor<IEnumerable<StorageLocation>>
    {
        public StorageLocationsResponseProcessor(IResourceBuilder<IEnumerable<StorageLocation>> resourceBuilder)
            : base(resourceBuilder, "storage-locations", 1)
        {
        }
    }
}
