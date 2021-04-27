namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Common.Reporting.Resources.Extensions;
    using Linn.Common.Reporting.Resources.ReportResultResources;

    public class ResultsModelsResourceBuilder : IResourceBuilder<IEnumerable<ResultsModel>>
    {
        public ReportReturnResource Build(IEnumerable<ResultsModel> resultsModels)
        {
            var returnResource = new ReportReturnResource();
            foreach (var resultsModel in resultsModels)
            {
                returnResource.ReportResults.Add(resultsModel.ConvertFinalModelToResource());
            }

            return returnResource;
        }

        public string GetLocation(IEnumerable<ResultsModel> model)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<IEnumerable<ResultsModel>>.Build(IEnumerable<ResultsModel> model)
        {
            return this.Build(model);
        }
    }
}
