namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.GoodsIn;

    public class ValidateStorageTypeResultResponseProcessor : JsonResponseProcessor<ValidateStorageTypeResult>
    {
        public ValidateStorageTypeResultResponseProcessor(IResourceBuilder<ValidateStorageTypeResult> resourceBuilder)
            : base(resourceBuilder, "validate-storage-type-result", 1)
        {
        }
    }
}
