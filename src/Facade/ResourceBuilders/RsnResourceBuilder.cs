namespace Linn.Stores.Facade.ResourceBuilders
{
    using Common.Facade;
    using Domain.LinnApps;
    using Resources;

    public class RsnResourceBuilder : IResourceBuilder<Rsn>
    {
        public string GetLocation(Rsn rsn)
        {
            return $"/logistics/rsns/{rsn.RsnNumber}";
        }

        object IResourceBuilder<Rsn>.Build(Rsn rsn)
        {
            return Build(rsn);
        }

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
    }
}
