namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class RsnResourceBuilder : IResourceBuilder<Rsn>
    {
        public RsnResource Build(Rsn rsn)
        {
            return new RsnResource
                       {
                           RsnNumber = rsn.RsnNumber,
                           ReasonCodeAlleged = rsn.ReasonCodeAlleged,
                           DateEntered = rsn.DateEntered?.ToString("o"),
                           Quantity = rsn.Quantity,
                           ArticleNumber = rsn.ArticleNumber,
                           AccountId = rsn.AccountId,
                           OutletName = rsn.OutletName,
                           OutletNumber = rsn.OutletNumber,
                           Country = rsn.Country,
                           CountryName = rsn.CountryName,
                           AccountType = rsn.AccountType
                       };
        }

        public string GetLocation(Rsn model)
        {
            throw new System.NotImplementedException();
        }

        object IResourceBuilder<Rsn>.Build(Rsn rsn) => this.Build(rsn);
    }
}