namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Wcs;
    using Linn.Stores.Resources;

    public class WarehouseLocationResourceBuilder : IResourceBuilder<WarehouseLocation>
    {
        public WarehouseLocationResource Build(WarehouseLocation model)
        {
            return new WarehouseLocationResource
                       {
                           Location = model.Location,
                           PalletId = model.PalletId
                       };
        }

        public string GetLocation(WarehouseLocation model)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<WarehouseLocation>.Build(WarehouseLocation location) => this.Build(location);
    }
}
