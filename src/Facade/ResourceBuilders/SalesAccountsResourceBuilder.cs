namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class SalesAccountsResourceBuilder : IResourceBuilder<IEnumerable<SalesAccount>>
    {
        private readonly SalesAccountResourceBuilder salesAccountResourceBuilder = new SalesAccountResourceBuilder();

        public IEnumerable<SalesAccountResource> Build(IEnumerable<SalesAccount> salesAccounts)
        {
            return salesAccounts.Select(s => this.salesAccountResourceBuilder.Build(s));
        }

        public string GetLocation(IEnumerable<SalesAccount> model)
        {
            throw new System.NotImplementedException();
        }

        object IResourceBuilder<IEnumerable<SalesAccount>>.Build(IEnumerable<SalesAccount> salesAccounts) =>
            this.Build(salesAccounts);
    }
}