namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class StoragePlaceResourceBuilder : IResourceBuilder<StoragePlace>
    {
        public StoragePlaceResource Build(StoragePlace storagePlace)
        {
            return new StoragePlaceResource
                       {
                           LocationId = storagePlace.LocationId,
                           PalletNumber = storagePlace.PalletNumber,
                           StoragePlaceDescription = storagePlace.StoragePlaceDescription,
                           Name = storagePlace.Name
                       };
        }

        public string GetLocation(StoragePlace model)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<StoragePlace>.Build(StoragePlace storagePlace) => this.Build(storagePlace);

        private IEnumerable<LinkResource> BuildLinks(StoragePlace storagePlace)
        {
            throw new NotImplementedException();
        }
    }
}