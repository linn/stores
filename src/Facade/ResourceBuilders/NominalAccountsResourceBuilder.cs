namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class NominalAccountsResourceBuilder : IResourceBuilder<IEnumerable<NominalAccount>>
    {
        private readonly NominalAccountResourceBuilder accountResourceBuilder 
            = new NominalAccountResourceBuilder();

        public IEnumerable<NominalAccountResource> Build(IEnumerable<NominalAccount> accounts)
        {
            return accounts
                .Select(a => this.accountResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<NominalAccount>>.Build(IEnumerable<NominalAccount> account) 
            => this.Build(account);

        public string GetLocation(IEnumerable<NominalAccount> account)
        {
            throw new NotImplementedException();
        }
    }
}
