namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Resources;

    public class StorageLocationResourceBuilder : IResourceBuilder<StorageLocation>
    {
        public StorageLocationResource Build(StorageLocation storageLocation)
        {
            return new StorageLocationResource
                       {
                           Id = storageLocation.LocationId,
                           LocationCode = storageLocation.LocationCode,
                           Description = storageLocation.Description
                       };
        }

        public string GetLocation(StorageLocation model)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<StorageLocation>.Build(StorageLocation storageLocation) => this.Build(storageLocation);

        private IEnumerable<LinkResource> BuildLinks(StorageLocation storageLocation)
        {
            throw new NotImplementedException();
        }
    }
}
