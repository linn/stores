namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Common.Reporting.Models;

    public class ResultsModelJsonResponseProcessor : JsonResponseProcessor<ResultsModel>
    {
        public ResultsModelJsonResponseProcessor(IResourceBuilder<ResultsModel> resourceBuilder)
            : base(resourceBuilder, "results-model", 1)
        {
        }
    }
}
