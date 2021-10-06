namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps;

    public class RsnsResponseProcessor : JsonResponseProcessor<IEnumerable<Rsn>>
    {
        public RsnsResponseProcessor(IResourceBuilder<IEnumerable<Rsn>> resourceBuilder)
            : base(resourceBuilder, "linnapps-rsns", 1)
        {
        }
    }
}
