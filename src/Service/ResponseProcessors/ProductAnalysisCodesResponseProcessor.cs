namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;


    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps;

    public class ProductAnalysisCodesResponseProcessor : JsonResponseProcessor<IEnumerable<ProductAnalysisCode>>
    {
        public ProductAnalysisCodesResponseProcessor(IResourceBuilder<IEnumerable<ProductAnalysisCode>> resourceBuilder)
            : base(resourceBuilder, "product-analysis-codes", 1)
        {
        }
    }
}
