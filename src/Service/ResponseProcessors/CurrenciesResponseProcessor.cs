namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps;

    public class CurrenciesResponseProcessor : JsonResponseProcessor<IEnumerable<Currency>>
    {
        public CurrenciesResponseProcessor(IResourceBuilder<IEnumerable<Currency>> resourceBuilder)
            : base(resourceBuilder, "linnapps-currencies", 1)
        {
        }
    }
}
