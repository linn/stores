namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class ProductAnalysisCodesResourceBuilder : IResourceBuilder<IEnumerable<ProductAnalysisCode>>
    {
        private readonly ProductAnalysisCodeResourceBuilder productAnalysisCodeResourceBuilder = new ProductAnalysisCodeResourceBuilder();

        public IEnumerable<ProductAnalysisCodeResource> Build(IEnumerable<ProductAnalysisCode> productAnalysisCodes)
        {
            return productAnalysisCodes
                .Select(a => this.productAnalysisCodeResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<ProductAnalysisCode>>.Build(IEnumerable<ProductAnalysisCode> productAnalysisCodes) => this.Build(productAnalysisCodes);

        public string GetLocation(IEnumerable<ProductAnalysisCode> productAnalysisCodes)
        {
            throw new NotImplementedException();
        }
    }
}