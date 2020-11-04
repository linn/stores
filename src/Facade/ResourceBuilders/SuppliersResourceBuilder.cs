namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class SuppliersResourceBuilder : IResourceBuilder<IEnumerable<Supplier>>
    {
        private readonly SupplierResourceBuilder supplierResourceBuilder = new SupplierResourceBuilder();

        public IEnumerable<SupplierResource> Build(IEnumerable<Supplier> suppliers)
        {
            return suppliers
                .Select(a => this.supplierResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<Supplier>>.Build(IEnumerable<Supplier> suppliers) => this.Build(suppliers);

        public string GetLocation(IEnumerable<Supplier> suppliers)
        {
            throw new NotImplementedException();
        }
    }
}
