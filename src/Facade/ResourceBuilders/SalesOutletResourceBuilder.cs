namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class SalesOutletResourceBuilder : IResourceBuilder<SalesOutlet>
    {
        public SalesOutletResource Build(SalesOutlet salesOutlet)
        {
            return new SalesOutletResource
                       {
                           AccountId = salesOutlet.AccountId,
                           OutletNumber = salesOutlet.OutletNumber,
                           Name = salesOutlet.Name,
                           SalesCustomerId = salesOutlet.SalesCustomerId,
                           CountryName = salesOutlet.CountryName,
                           CountryCode = salesOutlet.CountryCode,
                           Rsns = salesOutlet.Rsns?.Select(
                               rsn => new RsnResource
                                          {
                                              RsnNumber = rsn.RsnNumber,
                                              ReasonCodeAlleged = rsn.ReasonCodeAlleged,
                                              DateEntered = rsn.DateEntered.ToString("o"),
                                              Quantity = rsn.Quantity,
                                              ArticleNumber = rsn.ArticleNumber,
                                              AccountId = rsn.AccountId,
                                              OutletName = rsn.OutletName,
                                              OutletNumber = rsn.OutletNumber,
                                              Country = rsn.Country,
                                              CountryName = rsn.CountryName,
                                              AccountType = rsn.AccountType
                                          })
                       };
        }

        public string GetLocation(SalesOutlet model)
        {
            throw new System.NotImplementedException();
        }

        object IResourceBuilder<SalesOutlet>.Build(SalesOutlet salesOutlet) => this.Build(salesOutlet);
    }
}