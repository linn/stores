namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.StockLocators;

    public class StoragePlacesResourceBuilder : IResourceBuilder<IEnumerable<StoragePlace>>
    {
        private readonly StoragePlaceResourceBuilder storagePlaceResourceBuilder = new StoragePlaceResourceBuilder();

        public IEnumerable<StoragePlaceResource> Build(IEnumerable<StoragePlace> storagePlaces)
        {
            return storagePlaces.Select(s => this.storagePlaceResourceBuilder.Build(s));
        }

        object IResourceBuilder<IEnumerable<StoragePlace>>.Build(IEnumerable<StoragePlace> storagePlaces) =>
            this.Build(storagePlaces);

        public string GetLocation(IEnumerable<StoragePlace> storagePlaces)
        {
            throw new NotImplementedException();
        }
    }
}