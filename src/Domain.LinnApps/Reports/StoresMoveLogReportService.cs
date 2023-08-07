namespace Linn.Stores.Domain.LinnApps.Reports
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;

    public class StoresMoveLogReportService : IStoresMoveLogReportService
    {
        private readonly IQueryRepository<StoresMoveLog> repository;

        private readonly IReportingHelper reportingHelper;

        public StoresMoveLogReportService(IQueryRepository<StoresMoveLog> repository, IReportingHelper reportingHelper)
        {
            this.repository = repository;
            this.reportingHelper = reportingHelper;
        }

        public ResultsModel GetReport(DateTime fromDate, DateTime toDate, string partNumber, string transType, string location, string stockPool)
        {
            var moveLog = this.repository.FilterBy(
                l => l.PartNumber == partNumber && l.DateProcessed >= fromDate && l.DateProcessed <= toDate).ToList();

            var model = new ResultsModel
                            {
                                ReportTitle = new NameModel(
                                    $"{fromDate:ddMMMyy} - {fromDate:ddMMMyy} {partNumber} ")
                            };

            var columns = this.ModelColumns();

            model.AddSortedColumns(columns);

            var values = SetModelRows(moveLog);

            this.reportingHelper.AddResultsToModel(model, values, CalculationValueModelType.Quantity, true);

            return model;
        }

        private List<AxisDetailsModel> ModelColumns()
        {
            return new List<AxisDetailsModel>
                       {
                           new AxisDetailsModel("Part Number")
                               {
                                   SortOrder = 1, GridDisplayType = GridDisplayType.TextValue
                               },
                           new AxisDetailsModel("Date")
                               {
                                   SortOrder = 2, GridDisplayType = GridDisplayType.TextValue
                               },
                           new AxisDetailsModel("Req", "Req")
                               {
                                   SortOrder = 3, GridDisplayType = GridDisplayType.TextValue
                               },
                           new AxisDetailsModel("BudgetId", "Budget Id")
                               {
                                   SortOrder = 4, GridDisplayType = GridDisplayType.TextValue
                               },
                           new AxisDetailsModel("By", "By")
                               {
                                   SortOrder = 5, GridDisplayType = GridDisplayType.TextValue
                               },
                           new AxisDetailsModel("Trans", "Trans")
                               {
                                   SortOrder = 6, GridDisplayType = GridDisplayType.TextValue
                               },
                           new AxisDetailsModel("Qty") { SortOrder = 7, GridDisplayType = GridDisplayType.TextValue },
                           new AxisDetailsModel("From")
                               {
                                   SortOrder = 8, GridDisplayType = GridDisplayType.TextValue
                               },
                           new AxisDetailsModel("To")
                               {
                                   SortOrder = 9, GridDisplayType = GridDisplayType.TextValue
                               }
                       };
        }

        private List<CalculationValueModel> SetModelRows(IEnumerable<StoresMoveLog> moveLogs)
        {
            var values = new List<CalculationValueModel>();

            foreach (var moveLog in moveLogs)
            {
                values.Add(
                    new CalculationValueModel
                    {
                        RowId = moveLog.Id.ToString(),
                        TextDisplay = moveLog.PartNumber,
                        ColumnId = "Part Number"
                    });
                values.Add(
                    new CalculationValueModel
                    {
                        RowId = moveLog.Id.ToString(),
                        TextDisplay = moveLog.DateProcessed?.ToString("ddMMMyy HH:MI"),
                        ColumnId = "Date"
                    });
                values.Add(
                    new CalculationValueModel
                    {
                        RowId = moveLog.Id.ToString(),
                        TextDisplay = $"{moveLog.ReqNumber}/{moveLog.ReqLine}",
                        ColumnId = "Req"
                    });
                values.Add(
                    new CalculationValueModel
                    {
                        RowId = moveLog.Id.ToString(),
                        TextDisplay = moveLog.BudgetId.ToString(),
                        ColumnId = "BudgetId"
                    });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = moveLog.Id.ToString(),
                            TextDisplay = moveLog.CreatedBy,
                            ColumnId = "By"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = moveLog.Id.ToString(),
                            TextDisplay = moveLog.TransactionCode,
                            ColumnId = "Trans"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = moveLog.Id.ToString(),
                            TextDisplay = moveLog.Qty.ToString(),
                            ColumnId = "Qty"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = moveLog.Id.ToString(),
                            TextDisplay = string.IsNullOrEmpty(moveLog.FromLocation) ? string.Empty : $"{moveLog.FromLocation} {moveLog.FromBatchRef}/{moveLog.FromBatchDate.ToShortDateString()}",
                            ColumnId = "From"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = moveLog.Id.ToString(),
                            TextDisplay = $"{moveLog.ToLocation} {moveLog.ToBatchRef}/{moveLog.ToBatchDate.ToShortDateString()}",
                            ColumnId = "To"
                        });
            }

            return values;
        }
    }
}
