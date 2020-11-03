namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Common.Reporting.Resources.Extensions;
    using Linn.Common.Reporting.Resources.ReportResultResources;

    public class ResultsModelResourceBuilder : IResourceBuilder<ResultsModel>
    {
        public ReportReturnResource Build(ResultsModel resultsModel)
        {
            var returnResource = new ReportReturnResource();
            returnResource.ReportResults.Add(resultsModel.ConvertFinalModelToResource());

            return returnResource;
        }

        object IResourceBuilder<ResultsModel>.Build(ResultsModel model) => this.Build(model);

        public string GetLocation(ResultsModel model)
        {
            throw new NotImplementedException();
        }
    }
}