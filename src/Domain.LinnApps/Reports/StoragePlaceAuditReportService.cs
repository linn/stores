namespace Linn.Stores.Domain.LinnApps.Reports
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    public class StoragePlaceAuditReportService : IStoragePlaceAuditReportService
    {
        private readonly IReportingHelper reportingHelper;

        private readonly IRepository<Part, int> partsRepository;

        private readonly IRepository<StockLocator, int> stockLocatorRepository;

        private readonly IQueryRepository<StoragePlace> storagePlaceRepository;

        private readonly IQueryRepository<StoresBudget> storesBudgetsRepository;

        public StoragePlaceAuditReportService(
            IReportingHelper reportingHelper,
            IRepository<Part, int> partsRepository,
            IRepository<StockLocator, int> stockLocatorRepository,
            IQueryRepository<StoragePlace> storagePlaceRepository,
            IQueryRepository<StoresBudget> storesBudgetsRepository)
        {
            this.reportingHelper = reportingHelper;
            this.partsRepository = partsRepository;
            this.stockLocatorRepository = stockLocatorRepository;
            this.storagePlaceRepository = storagePlaceRepository;
            this.storesBudgetsRepository = storesBudgetsRepository;
        }

        public ResultsModel StoragePlaceAuditReport(IEnumerable<string> locationList, string locationRange)
        {
            List<StoragePlace> storagePlaces;

            if (!string.IsNullOrEmpty(locationRange) && locationRange.StartsWith("E-K"))
            {
                storagePlaces = this.storagePlaceRepository.FilterBy(s => s.Name.StartsWith(locationRange))
                    .OrderBy(s => s.Name).ToList();
            }
            else
            {
                storagePlaces = this.storagePlaceRepository.FilterBy(s => locationList.Any(l => s.Name == l))
                    .OrderBy(s => s.Name).ToList();
            }

            var stockLocators = this.stockLocatorRepository
                .FilterBy(
                    s => s.Quantity > 0 && storagePlaces.Any(
                             sp => sp.LocationId == s.LocationId && sp.PalletNumber == s.PalletNumber)).Select(
                    sl => new StockLocator
                              {
                                  Id = sl.Id,
                                  Quantity = sl.Quantity,
                                  PalletNumber = sl.PalletNumber,
                                  LocationId = sl.LocationId,
                                  BudgetId = sl.BudgetId,
                                  PartNumber = sl.PartNumber,
                                  QuantityAllocated = sl.QuantityAllocated
                              }).OrderBy(s => s.LocationId).ThenBy(s => s.PartNumber).ToList();

            var parts = this.partsRepository.FilterBy(p => stockLocators.Any(s => s.PartNumber == p.PartNumber)).Select(
                p => new Part
                         {
                             PartNumber = p.PartNumber,
                             Description = p.Description,
                             OurUnitOfMeasure = p.OurUnitOfMeasure,
                             RawOrFinished = p.RawOrFinished
                         }).ToList();

            var model = new ResultsModel { ReportTitle = new NameModel($"Storage Place: {locationRange}") };

            var columns = this.ModelColumns();

            model.AddSortedColumns(columns);

            var values = this.SetModelRows(storagePlaces, stockLocators, parts);

            this.reportingHelper.AddResultsToModel(model, values, CalculationValueModelType.Quantity, true);

            return model;
        }

        private List<CalculationValueModel> SetModelRows(
            IReadOnlyCollection<StoragePlace> storagePlaces,
            IEnumerable<StockLocator> stockLocators,
            IReadOnlyCollection<Part> parts)
        {
            var values = new List<CalculationValueModel>();

            foreach (var stockLocator in stockLocators)
            {
                var storagePlace = storagePlaces.FirstOrDefault(
                    sp => sp.LocationId == stockLocator.LocationId && sp.PalletNumber == stockLocator.PalletNumber);

                var quantity = stockLocator.Quantity;

                var quantityAllocated = stockLocator.QuantityAllocated;

                var part = parts.First(p => p.PartNumber == stockLocator.PartNumber);

                var rowId = $"{stockLocator.LocationId}{stockLocator.PalletNumber}{stockLocator.PartNumber}";

                values.Add(
                    new CalculationValueModel
                        {
                            RowId = rowId, TextDisplay = storagePlace.Name, ColumnId = "Storage Place"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = rowId, TextDisplay = part.PartNumber, ColumnId = "Part Number"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = rowId, TextDisplay = part.RawOrFinished, ColumnId = "Raw or Finished"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = rowId, TextDisplay = part.Description, ColumnId = "Description"
                        });
                values.Add(
                    new CalculationValueModel { RowId = rowId, TextDisplay = part.OurUnitOfMeasure, ColumnId = "UOM" });
                values.Add(
                    new CalculationValueModel { RowId = rowId, Quantity = quantity ?? 0, ColumnId = "Quantity" });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = rowId, Quantity = quantityAllocated ?? 0, ColumnId = "Allocated"
                        });
            }

            return values;
        }

        private List<AxisDetailsModel> ModelColumns()
        {
            return new List<AxisDetailsModel>
                       {
                           new AxisDetailsModel("Storage Place")
                               {
                                   SortOrder = 0, GridDisplayType = GridDisplayType.TextValue
                               },
                           new AxisDetailsModel("Part Number")
                               {
                                   SortOrder = 1, GridDisplayType = GridDisplayType.TextValue
                               },
                           new AxisDetailsModel("Raw or Finished")
                               {
                                   SortOrder = 2, GridDisplayType = GridDisplayType.TextValue
                               },
                           new AxisDetailsModel("Description")
                               {
                                   SortOrder = 3, GridDisplayType = GridDisplayType.TextValue
                               },
                           new AxisDetailsModel("Quantity") { SortOrder = 4, GridDisplayType = GridDisplayType.Value },
                           new AxisDetailsModel("UOM") { SortOrder = 5, GridDisplayType = GridDisplayType.TextValue },
                           new AxisDetailsModel("Allocated") { SortOrder = 6, GridDisplayType = GridDisplayType.Value }
                       };
        }
    }
}
