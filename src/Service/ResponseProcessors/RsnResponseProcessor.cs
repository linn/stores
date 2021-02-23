namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps;

    public class RsnResponseProcessor : JsonResponseProcessor<Rsn>
    {
        public RsnResponseProcessor(IResourceBuilder<Rsn> resourceBuilder)
            : base(resourceBuilder, "rsn", 1)
        {
        }
    }
}