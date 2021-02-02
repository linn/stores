namespace Linn.Stores.Domain.LinnApps.Allocation
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;

    public class AllocationReportsService : IAllocationReportsService
    {
        private readonly IQueryRepository<DespatchPickingSummary> despatchPickingSummaryRepository;

        private readonly IReportingHelper reportingHelper;

        public AllocationReportsService(
            IQueryRepository<DespatchPickingSummary> despatchPickingSummaryRepository,
            IReportingHelper reportingHelper)
        {
            this.despatchPickingSummaryRepository = despatchPickingSummaryRepository;
            this.reportingHelper = reportingHelper;
        }

        public ResultsModel DespatchPickingSummary()
        {
            var resultsModel = new ResultsModel
                                   {
                                       ReportTitle = new NameModel("Despatch Picking Summary")
                                   };

            var columns = new List<AxisDetailsModel>
                              {
                                  new AxisDetailsModel("From", GridDisplayType.TextValue),
                                  new AxisDetailsModel("Qty", GridDisplayType.Value),
                                  new AxisDetailsModel("Article Number", GridDisplayType.TextValue)
                                      {
                                          AllowWrap = false
                                      },
                                  new AxisDetailsModel("Invoice Description", GridDisplayType.TextValue),
                                  new AxisDetailsModel("Addressee", GridDisplayType.TextValue),
                                  new AxisDetailsModel("Location", GridDisplayType.TextValue),
                                  new AxisDetailsModel("Empty", GridDisplayType.TextValue)
                              };

            resultsModel.AddSortedColumns(columns);

            var summary = this.despatchPickingSummaryRepository.FindAll().OrderBy(a => a.FromPlace).ToList();

            var models = new List<CalculationValueModel>();
            var rowNumber = 0;
            foreach (var summaryLine in summary)
            {
                rowNumber++;
                var rowId = $"{rowNumber:000}";
                models.Add(
                    new CalculationValueModel
                        {
                            RowId = rowId,
                            ColumnId = "From",
                            TextDisplay = summaryLine.FromPlace
                        });
                models.Add(
                    new CalculationValueModel
                        {
                            RowId = rowId,
                            ColumnId = "Qty",
                            Value = summaryLine.Quantity
                        });
                models.Add(
                    new CalculationValueModel
                        {
                            RowId = rowId,
                            ColumnId = "Article Number",
                            TextDisplay = summaryLine.ArticleNumber
                        });
                models.Add(
                    new CalculationValueModel
                        {
                            RowId = rowId,
                            ColumnId = "Invoice Description",
                            TextDisplay = summaryLine.InvoiceDescription
                    });
                models.Add(
                    new CalculationValueModel
                        {
                            RowId = rowId,
                            ColumnId = "Addressee",
                            TextDisplay = summaryLine.Addressee
                    });
                models.Add(
                    new CalculationValueModel
                        {
                            RowId = rowId,
                            ColumnId = "Location",
                            TextDisplay = summaryLine.Location
                    });
                models.Add(
                    new CalculationValueModel
                        {
                            RowId = rowId,
                            ColumnId = "Empty",
                            TextDisplay = summaryLine.QtyNeededFromLocation == summaryLine.QuantityOfItemsAtLocation
                                              ? "Empty"
                                              : string.Empty
                        });
            }

            this.reportingHelper.AddResultsToModel(resultsModel, models, CalculationValueModelType.Value, true);

            return resultsModel;
        }
    }
}
