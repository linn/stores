namespace Linn.Stores.Domain.LinnApps.Tqms
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;

    public class TqmsReportsService : ITqmsReportsService
    {
        private readonly IReportingHelper reportingHelper;

        private readonly IQueryRepository<TqmsSummaryByCategory> tqmsSummaryByCategoryQueryRepository;

        private readonly IQueryRepository<TqmsOutstandingLoansByCategory> tqmsOutstandingLoansByCategoryRepository;

        private readonly IRepository<TqmsJobRef, string> tqmsJobRefsRepository;

        public TqmsReportsService(
            IReportingHelper reportingHelper,
            IQueryRepository<TqmsSummaryByCategory> tqmsSummaryByCategoryQueryRepository,
            IQueryRepository<TqmsOutstandingLoansByCategory> tqmsOutstandingLoansByCategoryRepository,
            IRepository<TqmsJobRef, string> tqmsJobRefsRepository)
        {
            this.reportingHelper = reportingHelper;
            this.tqmsSummaryByCategoryQueryRepository = tqmsSummaryByCategoryQueryRepository;
            this.tqmsOutstandingLoansByCategoryRepository = tqmsOutstandingLoansByCategoryRepository;
            this.tqmsJobRefsRepository = tqmsJobRefsRepository;
        }

        public IEnumerable<ResultsModel> TqmsSummaryByCategoryReport(string jobRef)
        {
            var stock = this.tqmsSummaryByCategoryQueryRepository.FilterBy(t => t.JobRef == jobRef);
            var loan = this.tqmsOutstandingLoansByCategoryRepository.FilterBy(t => t.JobRef == jobRef);

            var jobRefDetails = this.tqmsJobRefsRepository.FindById(jobRef);

            var summaryResultsModel = new ResultsModel
                                          {
                                              ReportTitle = new NameModel($"Total Stock Summary {jobRefDetails.DateOfRun:dd-MMM-yyyy} ({jobRefDetails.JobRef})")
                                          };
            summaryResultsModel.AddSortedColumns(new List<AxisDetailsModel>
                                                     {
                                                         new AxisDetailsModel("StockType", GridDisplayType.TextValue),
                                                         new AxisDetailsModel("Value", GridDisplayType.Value)
                                                     });
            summaryResultsModel.AddRow("Total Stock");
            summaryResultsModel.AddRow("Loan Stock Value");
            summaryResultsModel.SetGridTextValue(0, 0, "Stock Value");
            summaryResultsModel.SetGridValue(0, 1, stock.Sum(a => a.TotalValue));
            summaryResultsModel.SetGridTextValue(1, 0, "Loan Stock Value");
            summaryResultsModel.SetGridValue(1, 1, loan.Sum(a => a.TotalStoresValue));

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

            return new List<ResultsModel> { summaryResultsModel, resultsModel };
        }
    }
}
