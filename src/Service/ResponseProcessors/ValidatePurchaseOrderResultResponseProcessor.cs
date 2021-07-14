namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps;

    public class ValidatePurchaseOrderResultResponseProcessor : JsonResponseProcessor<ValidatePurchaseOrderResult>
    {
        public ValidatePurchaseOrderResultResponseProcessor(IResourceBuilder<ValidatePurchaseOrderResult> resourceBuilder)
            : base(resourceBuilder, "validate-purchase-order-result", 1)
        {
        }
    }
}
