namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.StockLocators;

    public class StorageLocationsResourceBuilder : IResourceBuilder<IEnumerable<StorageLocation>>
    {
        private readonly StorageLocationResourceBuilder storageLocationResourceBuilder = new StorageLocationResourceBuilder();

        public IEnumerable<StorageLocationResource> Build(IEnumerable<StorageLocation> storageLocations)
        {
            return storageLocations.Select(s => this.storageLocationResourceBuilder.Build(s));
        }

        object IResourceBuilder<IEnumerable<StorageLocation>>.Build(IEnumerable<StorageLocation> storageLocations) =>
            this.Build(storageLocations);

        public string GetLocation(IEnumerable<StorageLocation> storageLocations)
        {
            throw new NotImplementedException();
        }
    }
}
