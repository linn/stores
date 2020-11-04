namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps;

    public class CountriesResponseProcessor : JsonResponseProcessor<IEnumerable<Country>>
    {
        public CountriesResponseProcessor(IResourceBuilder<IEnumerable<Country>> resourceBuilder)
            : base(resourceBuilder, "linnapps-countries", 1)
        {
        }
    }
}
