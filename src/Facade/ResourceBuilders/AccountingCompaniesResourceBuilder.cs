namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class AccountingCompaniesResourceBuilder : IResourceBuilder<IEnumerable<AccountingCompany>>
    {
        private readonly AccountingCompanyResourceBuilder accountingCompaniesResourceBuilder = 
            new AccountingCompanyResourceBuilder();

        public IEnumerable<AccountingCompanyResource> Build(IEnumerable<AccountingCompany> accountingCompanies)
        {
            return accountingCompanies
                .Select(a => this.accountingCompaniesResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<AccountingCompany>>.Build(IEnumerable<AccountingCompany> accountingCompaniess) => this.Build(accountingCompaniess);

        public string GetLocation(IEnumerable<AccountingCompany> accountingCompaniess)
        {
            throw new NotImplementedException();
        }
    }
}