namespace Linn.Stores.Facade.ResourceBuilders
{
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
                           CountryCode = salesOutlet.CountryCode
                       };
        }

        public string GetLocation(SalesOutlet model)
        {
            throw new System.NotImplementedException();
        }

        object IResourceBuilder<SalesOutlet>.Build(SalesOutlet salesOutlet) => this.Build(salesOutlet);
    }
}