namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class AccountingCompanyResourceBuilder : IResourceBuilder<AccountingCompany>
    {
        public AccountingCompanyResource Build(AccountingCompany model)
        {
            return new AccountingCompanyResource { Name = model.Name, Description = model.Description };
        }

        public string GetLocation(AccountingCompany model)
        {
            throw new System.NotImplementedException();
        }

        object IResourceBuilder<AccountingCompany>.Build(AccountingCompany accountingCompany) => this.Build(accountingCompany);
    }
}
