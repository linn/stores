namespace Linn.Stores.Domain.LinnApps.Tqms
{
    using System.Collections.Generic;
    using System.Data;

    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;

    public class TqmsReportsService : ITqmsReportsService
    {
        private readonly IReportingHelper reportingHelper;

        private readonly IQueryRepository<TqmsSummaryByCategory> tqmsSummaryByCategoryQueryRepository;

        public TqmsReportsService(
            IReportingHelper reportingHelper,
            IQueryRepository<TqmsSummaryByCategory> tqmsSummaryByCategoryQueryRepository)
        {
            this.reportingHelper = reportingHelper;
            this.tqmsSummaryByCategoryQueryRepository = tqmsSummaryByCategoryQueryRepository;
        }

        public IEnumerable<ResultsModel> TqmsSummaryByCategoryReport(string jobRef)
        {
            var stock = this.tqmsSummaryByCategoryQueryRepository.FilterBy(t => t.JobRef == jobRef);

            var resultsModel = new ResultsModel { ReportTitle = new NameModel("TQMS Summary") };
            var columns = new List<AxisDetailsModel>
                               {
                                   new AxisDetailsModel("Heading", GridDisplayType.TextValue),
                                   new AxisDetailsModel("Category", GridDisplayType.TextValue),
                                   new AxisDetailsModel("Value", GridDisplayType.Value)
                               };
            resultsModel.AddSortedColumns(columns);
            var models = new List<CalculationValueModel>();
            var rowNumber = 0;
            foreach (var tqmsSummaryByCategory in stock)
            {
                rowNumber++;
                var rowId = $"{rowNumber:000}";
                models.Add(
                    new CalculationValueModel
                        {
                            RowId = rowId,
                            ColumnId = "Heading",
                            TextDisplay = tqmsSummaryByCategory.HeadingDescription
                        });
                models.Add(
                    new CalculationValueModel
                        {
                            RowId = rowId,
                            ColumnId = "Category",
                            TextDisplay = tqmsSummaryByCategory.CategoryDescription
                        }); 
                models.Add(
                    new CalculationValueModel
                        {
                            RowId = rowId,
                            ColumnId = "Value",
                            Value = tqmsSummaryByCategory.TotalValue
                        });
            }

            this.reportingHelper.AddResultsToModel(resultsModel, models, CalculationValueModelType.Value, true);
            this.reportingHelper.SubtotalRowsByTextColumnValue(resultsModel, 0, new[] { 2 }, false);
            this.reportingHelper.RemovedRepeatedValues(resultsModel, 0, new[] { 0 });

            return new List<ResultsModel> { resultsModel };
        }
    }
}
