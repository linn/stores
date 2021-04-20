namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class PartStorageTypesResourceBuilder : IResourceBuilder<IEnumerable<PartStorageType>>
    {
        private readonly PartStorageTypeResourceBuilder partStorageTypeResourceBuilder = new PartStorageTypeResourceBuilder();

        public IEnumerable<PartStorageTypeResource> Build(IEnumerable<PartStorageType> partStorageTypes)
        {
            return partStorageTypes
                .OrderBy(b => b.StorageType)
                .Select(a => this.partStorageTypeResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<PartStorageType>>.Build(IEnumerable<PartStorageType> partStorageTypes) => this.Build(partStorageTypes);

        public string GetLocation(IEnumerable<PartStorageType> partStorageTypes)
        {
            throw new NotImplementedException();
        }
    }
}
