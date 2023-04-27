namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;

    using Linn.Stores.Domain.LinnApps.Wcs;

    public class WarehouseLocationResponseProcessor : JsonResponseProcessor<WarehouseLocation>
    {
        public WarehouseLocationResponseProcessor(IResourceBuilder<WarehouseLocation> resourceBuilder)
            : base(resourceBuilder, "warehouse-location", 1)
        {
        }
    }
}
