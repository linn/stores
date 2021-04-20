namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps;

    public class PartStorageTypesResponseProcessor : JsonResponseProcessor<IEnumerable<PartStorageType>>
    {
        public PartStorageTypesResponseProcessor(IResourceBuilder<IEnumerable<PartStorageType>> resourceBuilder)
            : base(resourceBuilder, "part-storage-types", 1)
        {
        }
    }
}
