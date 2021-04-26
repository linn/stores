namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Tpk.Models;

    public class TpkResultsResponseProcessor : JsonResponseProcessor<TpkResult>
    {
        public TpkResultsResponseProcessor(IResourceBuilder<TpkResult> resourceBuilder)
            : base(resourceBuilder, "tpk-result", 1)
        {
        }
    }
}
