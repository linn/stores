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
            var filteredMoveLog = this.GetReportData(fromDate, toDate, partNumber, transType, location, stockPool);

            var model = new ResultsModel
                            {
                                ReportTitle = new NameModel(
                                    $"{fromDate:ddMMMyy} - {toDate:ddMMMyy} {partNumber}{this.FiltersTitle(transType,location,stockPool)}")
                            };

            var columns = this.ModelColumns();

            model.AddSortedColumns(columns);

            var values = SetModelRows(filteredMoveLog);

            this.reportingHelper.AddResultsToModel(model, values, CalculationValueModelType.Quantity, true);

            return model;
        }

        private string FiltersTitle(string transType, string location, string stockPool)
        {
            var title = string.Empty;
            if (!string.IsNullOrEmpty(transType))
            {
                title += $" trans {transType}";
            }

            if (!string.IsNullOrEmpty(location))
            {
                title += $" location {location}";
            }

            if (!string.IsNullOrEmpty(stockPool))
            {
                title += $" stock pool {stockPool}";
            }

            return title;
        }

        private IList<StoresMoveLog> GetReportData(DateTime fromDate, DateTime toDate, string partNumber, string transType, string location, string stockPool)
        {
            if (!string.IsNullOrEmpty(partNumber))
            {
                var partMoveLog = this.repository.FilterBy(
                    l => l.PartNumber == partNumber && l.DateProcessed >= fromDate && l.DateProcessed <= toDate).ToList();

                return partMoveLog.Where(m => this.MatchesFilter(m, transType, location, stockPool)).ToList();
            }

            // for speed reasons
            if (!string.IsNullOrEmpty(location))
            {
                var locationMoveLog = this.repository.FilterBy(
                    l => l.DateProcessed >= fromDate && l.DateProcessed <= toDate && (l.FromLocation == location || l.ToLocation == location)).ToList();

                return locationMoveLog.Where(m => this.MatchesFilter(m, transType, string.Empty, stockPool)).ToList();
            }

            // this could be massive
            var moveLog = this.repository.FilterBy(
                l => l.DateProcessed >= fromDate && l.DateProcessed <= toDate).ToList();

            var filteredMoveLog = moveLog.Where(m => this.MatchesFilter(m, transType, location, stockPool)).ToList();

            return filteredMoveLog;
        }

        private bool MatchesFilter(StoresMoveLog moveLog, string transType, string location, string stockPool)
        {
            if (!moveLog.MatchesLocation(location))
            {
                return false;
            }

            if (!string.IsNullOrEmpty(stockPool))
            {
                if (moveLog.FromStockPool != stockPool && moveLog.ToStockPool != stockPool)
                {
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(transType))
            {
                if (moveLog.TransactionCode != transType.ToUpper())
                {
                    return false;
                }
            }

            return true;
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
                                   SortOrder = 8, GridDisplayType = GridDisplayType.TextValue, Name = "From"
                               },
                           new AxisDetailsModel("FromDetails")
                               {
                                   SortOrder = 9, GridDisplayType = GridDisplayType.TextValue, Name = "Batch - Stock Pool - State"
                               },
                           new AxisDetailsModel("To")
                               {
                                   SortOrder = 10, GridDisplayType = GridDisplayType.TextValue, Name = "To"
                               },
                           new AxisDetailsModel("ToDetails")
                               {
                                   SortOrder = 11, GridDisplayType = GridDisplayType.TextValue, Name = "Batch - Stock Pool - State"
                               },
                           new AxisDetailsModel("QIS")
                               {
                                   SortOrder = 12, GridDisplayType = GridDisplayType.TextValue, Name = "QIS"
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
                            TextDisplay = moveLog.FromLocation,
                            ColumnId = "From"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = moveLog.Id.ToString(),
                            TextDisplay = string.IsNullOrEmpty(moveLog.FromLocation) ? string.Empty : $"{moveLog.FromBatchRef}/{moveLog.FromBatchDate.ToShortDateString()} - {moveLog.FromStockPool} - {moveLog.FromState}",
                            ColumnId = "FromDetails"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = moveLog.Id.ToString(),
                            TextDisplay = moveLog.ToLocation,
                            ColumnId = "To"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = moveLog.Id.ToString(),
                            TextDisplay = string.IsNullOrEmpty(moveLog.ToLocation) ? string.Empty : $"{moveLog.ToBatchRef}/{moveLog.ToBatchDate.ToShortDateString()} - {moveLog.ToStockPool} - {moveLog.ToState}",
                            ColumnId = "ToDetails"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = moveLog.Id.ToString(),
                            TextDisplay = moveLog.QtyInStock.ToString(),
                            ColumnId = "QIS"
                        });
            }

            return values;
        }
    }
}
