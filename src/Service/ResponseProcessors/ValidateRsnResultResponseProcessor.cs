namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.GoodsIn;

    public class ValidateRsnResultResponseProcessor : JsonResponseProcessor<ValidateRsnResult>
    {
        public ValidateRsnResultResponseProcessor(IResourceBuilder<ValidateRsnResult> resourceBuilder)
            : base(resourceBuilder, "validate-purchase-order-result", 1)
        {
        }
    }
}
