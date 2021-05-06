namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class SupplierResourceBuilder : IResourceBuilder<Supplier>
    {
        public SupplierResource Build(Supplier supplier)
        {
            return new SupplierResource
                       {
                           Id = supplier.Id,
                           Name = supplier.Name,
                           CountryCode = supplier.CountryCode,
                           ApprovedCarrier = supplier.ApprovedCarrier
                       };
        }

        public string GetLocation(Supplier supplier)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<Supplier>.Build(Supplier supplier) => this.Build(supplier);

        private IEnumerable<LinkResource> BuildLinks(Supplier supplier)
        {
            throw new NotImplementedException();
        }
    }
}
