namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class SalesAccountResourceBuilder : IResourceBuilder<SalesAccount>
    {
        public SalesAccountResource Build(SalesAccount salesAccount)
        {
            return new SalesAccountResource
                       {
                           AccountId = salesAccount.AccountId,
                           AccountType = salesAccount.AccountType,
                           AccountName = salesAccount.AccountName,
                           DateClosed = salesAccount.DateClosed?.ToString("o")
                       };
        }

        public string GetLocation(SalesAccount model)
        {
            throw new System.NotImplementedException();
        }

        object IResourceBuilder<SalesAccount>.Build(SalesAccount salesAccount) => this.Build(salesAccount);
    }
}