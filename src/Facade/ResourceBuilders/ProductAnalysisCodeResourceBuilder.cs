namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class ProductAnalysisCodeResourceBuilder : IResourceBuilder<ProductAnalysisCode>
    {
        public ProductAnalysisCodeResource Build(ProductAnalysisCode productAnalysisCode)
        {
            return new ProductAnalysisCodeResource
                       {
                           ProductCode = productAnalysisCode.ProductCode,
                           Description = productAnalysisCode.Description,
                       };
        }

        public string GetLocation(ProductAnalysisCode productAnalysisCode)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<ProductAnalysisCode>.Build(ProductAnalysisCode productAnalysisCode) => this.Build(productAnalysisCode);

        private IEnumerable<LinkResource> BuildLinks(ProductAnalysisCode productAnalysisCode)
        {
            throw new NotImplementedException();
        }
    }
}
