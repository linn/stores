namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps;

    public class SalesAccountResponseProcessor : JsonResponseProcessor<SalesAccount>
    {
        public SalesAccountResponseProcessor(IResourceBuilder<SalesAccount> resourceBuilder)
            : base(resourceBuilder, "sales-account", 1)
        {
        }
    }
}