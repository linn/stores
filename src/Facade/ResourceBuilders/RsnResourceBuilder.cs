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
                           InvoiceDescription = rsn.SalesArticle.Description,
                           Quantity = rsn.Quantity,
                           TariffCode = rsn.SalesArticle.Tariff.TariffCode,
                           Weight = rsn.SalesArticle.Weight ?? 0
                       };
        }

        public string GetLocation(Rsn rsn)
        {
            return $"/logistics/rsns/{rsn.RsnNumber}";
        }

        object IResourceBuilder<Rsn>.Build(Rsn rsn)
        {
            return this.Build(rsn);
        }
    }
}
