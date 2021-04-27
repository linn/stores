namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Tqms;

    public class TqmsJobRefsResponseProcessor : JsonResponseProcessor<IEnumerable<TqmsJobRef>>
    {
        public TqmsJobRefsResponseProcessor(IResourceBuilder<IEnumerable<TqmsJobRef>> resourceBuilder)
            : base(resourceBuilder, "tqms-jobrefs", 1)
        {
        }
    }
}
