namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class CurrencyResourceBuilder : IResourceBuilder<Currency>
    {
        public CurrencyResource Build(Currency currency)
        {
            return new CurrencyResource
                       {    
                           Code = currency.Code,
                           Name = currency.Name,
                           Links = this.BuildLinks(currency).ToArray()
                       };
        }

        public string GetLocation(Currency currency)
        {
            return $"/logistics/currencies/{currency.Code}";
        }

        object IResourceBuilder<Currency>.Build(Currency currency) => this.Build(currency);

        private IEnumerable<LinkResource> BuildLinks(Currency currency)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(currency) };
        }
    }
}
