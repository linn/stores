namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class SalesOutletsResourceBuilder : IResourceBuilder<IEnumerable<SalesOutlet>>
    {
        private readonly SalesOutletResourceBuilder salesOutletResourceBuilder = new SalesOutletResourceBuilder();

        public IEnumerable<SalesOutletResource> Build(IEnumerable<SalesOutlet> salesOutlets)
        {
            return salesOutlets.Select(s => this.salesOutletResourceBuilder.Build(s));
        }

        public string GetLocation(IEnumerable<SalesOutlet> model)
        {
            throw new System.NotImplementedException();
        }

        object IResourceBuilder<IEnumerable<SalesOutlet>>.Build(IEnumerable<SalesOutlet> salesOutlets) =>
            this.Build(salesOutlets);
    }
}