namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps;

    public class SalesOutletResponseProcessor : JsonResponseProcessor<SalesOutlet>
    {
        public SalesOutletResponseProcessor(IResourceBuilder<SalesOutlet> resourceBuilder)
            : base(resourceBuilder, "sales-outlet", 1)
        {
        }
    }
}