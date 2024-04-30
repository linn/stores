namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class AccountingCompanyResourceBuilder : IBuilder<AccountingCompany>
    {
        public AccountingCompanyResource Build(AccountingCompany model, IEnumerable<string> claims)
        {
            return new AccountingCompanyResource { Name = model.Name, Description = model.Description };
        }

        public string GetLocation(AccountingCompany model)
        {
            throw new System.NotImplementedException();
        }

        object IBuilder<AccountingCompany>.Build(AccountingCompany accountingCompany, IEnumerable<string> claims) => this.Build(accountingCompany, claims);
    }
}
