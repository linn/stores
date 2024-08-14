namespace Linn.Stores.Domain.LinnApps.Reports
{
    using System.Collections.Generic;

    using Linn.Common.Reporting.Models;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    public class StockReportService : IStockReportService
    {
        private readonly IFilterByWildcardRepository<StockLocator, int> stockLocatorRepository;

        private readonly IReportingHelper reportingHelper;

        public StockReportService(
            IFilterByWildcardRepository<StockLocator, int> stockLocatorRepository,
            IReportingHelper reportingHelper)
        {
            this.stockLocatorRepository = stockLocatorRepository;
            this.reportingHelper = reportingHelper;
        }

        public ResultsModel GetStockLocatorReport(string siteCode)
        {
            var locators = this.stockLocatorRepository
                .FilterBy(a => a.StorageLocation.SiteCode == siteCode && a.Quantity > 0);

            var report = new ResultsModel { ReportTitle = new NameModel($"Stock at site {siteCode}") };
            var values = new List<CalculationValueModel>();

            var columns = new List<AxisDetailsModel>
                              {
                                  new AxisDetailsModel("Location")
                                      {
                                          SortOrder = 10, GridDisplayType = GridDisplayType.TextValue
                                      },
                                  new AxisDetailsModel("Pallet Number")
                                      {
                                          SortOrder = 20, GridDisplayType = GridDisplayType.TextValue
                                      },
                                  new AxisDetailsModel("Part Number")
                                      {
                                          SortOrder = 30, GridDisplayType = GridDisplayType.TextValue
                                      },
                                  new AxisDetailsModel("Quantity")
                                      {
                                          SortOrder = 40, GridDisplayType = GridDisplayType.Value
                                      },
                                  new AxisDetailsModel("State")
                                      {
                                          SortOrder = 50, GridDisplayType = GridDisplayType.TextValue
                                      },
                                  new AxisDetailsModel("Batch Ref")
                                      {
                                          SortOrder = 60, GridDisplayType = GridDisplayType.TextValue
                                      }
                              };
            report.AddSortedColumns(columns);

            foreach (var stockLocator in locators)
            {
                var rowId = stockLocator.Id.ToString();
                values.Add(new CalculationValueModel
                               {
                                   RowId = rowId,
                                   TextDisplay = stockLocator.StorageLocation.LocationCode,
                                   ColumnId = "Location"
                               });
                values.Add(new CalculationValueModel
                               {
                                   RowId = rowId,
                                   TextDisplay = stockLocator.PalletNumber?.ToString(),
                                   ColumnId = "Pallet Number"
                               });
                values.Add(new CalculationValueModel
                               {
                                   RowId = rowId,
                                   TextDisplay = stockLocator.PartNumber,
                                   ColumnId = "Part Number"
                               });
                values.Add(new CalculationValueModel
                               {
                                   RowId = rowId,
                                   Value = stockLocator.Quantity.GetValueOrDefault(),
                                   ColumnId = "Quantity"
                               });
                values.Add(new CalculationValueModel
                               {
                                   RowId = rowId,
                                   TextDisplay = stockLocator.State,
                                   ColumnId = "State"
                               });
                values.Add(new CalculationValueModel
                               {
                                   RowId = rowId,
                                   TextDisplay = stockLocator.BatchRef,
                                   ColumnId = "Batch Ref"
                               });
            }

            this.reportingHelper.AddResultsToModel(report, values, CalculationValueModelType.Value, true);
            this.reportingHelper.SortRowsByTextColumnValues(report, report.ColumnIndex("Location"));
            return report;
        }
    }
}
