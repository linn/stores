namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Common.Reporting.Models;

    public class ResultsModelsJsonResponseProcessor : JsonResponseProcessor<IEnumerable<ResultsModel>>
    {
        public ResultsModelsJsonResponseProcessor(IResourceBuilder<IEnumerable<ResultsModel>> resourceBuilder)
            : base(resourceBuilder, "results-model", 1)
        {
        }
    }
}
