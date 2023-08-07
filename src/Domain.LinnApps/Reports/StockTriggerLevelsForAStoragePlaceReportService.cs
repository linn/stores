namespace Linn.Stores.Domain.LinnApps.Reports
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Layouts;
    using Linn.Common.Reporting.Models;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    public class StockTriggerLevelsForAStoragePlaceReportService : IStockTriggerLevelsForAStoragePlaceReportService
    {
        private readonly IQueryRepository<StoragePlace> storagePlaces;

        private readonly IStockTriggerLevelsRepository triggerLevels;

        private readonly IReportingHelper reportingHelper;

        public StockTriggerLevelsForAStoragePlaceReportService(
            IQueryRepository<StoragePlace> storagePlaces,
            IStockTriggerLevelsRepository triggerLevels,
            IReportingHelper reportingHelper)
        {
            this.storagePlaces = storagePlaces;
            this.triggerLevels = triggerLevels;
            this.reportingHelper = reportingHelper;
        }

        public ResultsModel GetReport(string location)
        {
            var place = this.storagePlaces.FindBy(x => x.Name == location.Trim().ToUpper());

            var triggers = place.PalletNumber.HasValue ? 
                                 this.triggerLevels.FilterBy(x => x.PalletNumber == place.PalletNumber)
                               : this.triggerLevels.FilterBy(x => x.LocationId == place.LocationId)
                                     .OrderBy(l => l.PartNumber);

            var reportLayout = new SimpleGridLayout(
                this.reportingHelper,
                CalculationValueModelType.Value,
                null,
                $"Stock Triggerl Levels at {place?.Name}");
            reportLayout.AddColumnComponent(
                null,
                new List<AxisDetailsModel>
                    {
                        new AxisDetailsModel("Part", "Part", GridDisplayType.TextValue),
                        new AxisDetailsModel("TriggerLevel", "Trigger Level", GridDisplayType.TextValue),
                        new AxisDetailsModel("KanbanSize", "Kanban Size", GridDisplayType.TextValue),
                        new AxisDetailsModel("MaxCap", "MaxCap", GridDisplayType.TextValue)
                    });

            var values = new List<CalculationValueModel>();

            foreach (var trigger in triggers)
            {
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = trigger.PartNumber,
                            ColumnId = "Part",
                            TextDisplay = trigger.PartNumber
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = trigger.PartNumber,
                            ColumnId = "TriggerLevel",
                            TextDisplay = trigger.TriggerLevel.ToString()
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = trigger.PartNumber,
                            ColumnId = "KanbanSize",
                            TextDisplay = trigger.KanbanSize.ToString()
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = trigger.PartNumber,
                            ColumnId = "MaxCap",
                            TextDisplay = trigger.MaxCapacity.ToString()
                        });
            }

            reportLayout.SetGridData(values);
            return reportLayout.GetResultsModel();
        }
    }
}
