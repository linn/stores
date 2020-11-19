namespace Linn.Stores.Domain.LinnApps.Reports
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Common.Reporting.Models;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class StoragePlaceAuditReportService : IStoragePlaceAuditReportService
    {
        private readonly IReportingHelper reportingHelper;

        private readonly IRepository<Part, int> partsRepository;

        private readonly IQueryRepository<StockLocator> stockLocatorRepository;

        private readonly IQueryRepository<StoragePlace> storagePlaceRepository;

        private readonly IQueryRepository<StoresBudget> storesBudgetsRepository;

        public StoragePlaceAuditReportService(
            IReportingHelper reportingHelper,
            IRepository<Part, int> partsRepository,
            IQueryRepository<StockLocator> stockLocatorRepository,
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

            var stockLocators = this.stockLocatorRepository.FilterBy(
                s => storagePlaces.Any(sp => sp.LocationId == s.LocationId)
                     && storagePlaces.Any(sp => sp.PalletNumber == s.PalletNumber)).Where(s => s.Quantity > 0).ToList();

            var storesBudgets = this.storesBudgetsRepository
                .FilterBy(s => stockLocators.Any(sl => sl.BudgetId == s.BudgetId)).ToList();

            var parts = this.partsRepository.FilterBy(p => stockLocators.Any(s => s.PartNumber == p.PartNumber))
                .ToList();

            var model = new ResultsModel { ReportTitle = new NameModel($"Storage Place: {locationRange}") };

            var columns = this.ModelColumns();

            model.AddSortedColumns(columns);

            var values = this.SetModelRows(storagePlaces, stockLocators, parts);

            this.reportingHelper.AddResultsToModel(model, values, CalculationValueModelType.Quantity, true);

            return model;
        }

        private List<CalculationValueModel> SetModelRows(
            IEnumerable<StoragePlace> storagePlaces,
            IEnumerable<StockLocator> stockLocators,
            IEnumerable<Part> parts)
        {
            var values = new List<CalculationValueModel>();

            foreach (var storagePlace in storagePlaces)
            {
                var locators = stockLocators
                    .Where(sl => sl.LocationId == storagePlace.LocationId && sl.PalletNumber == storagePlace.PalletNumber);

                var quantity = locators.Sum(l => l.Quantity);

                var quantityAllocated = locators.Sum(l => l.QuantityAllocated);
                
                var part = parts.First(p => p.PartNumber == locators.First().PartNumber);

                values.Add(
                    new CalculationValueModel
                        {
                            RowId = storagePlace.Name,
                            TextDisplay = storagePlace.Name,
                            ColumnId = "Storage Place"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = storagePlace.Name,
                            TextDisplay = storagePlace.StoragePlaceDescription,
                            ColumnId = "Description"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = storagePlace.Name,
                            TextDisplay = part.PartNumber,
                            ColumnId = "Part Number"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = storagePlace.Name,
                            TextDisplay = part.Description,
                            ColumnId = "Part Description"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = storagePlace.Name,
                            TextDisplay = part.OurUnitOfMeasure,
                            ColumnId = "UOM"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = storagePlace.Name,
                            TextDisplay = part.RawOrFinished,
                            ColumnId = "Raw or Finished"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = storagePlace.Name, Quantity = quantity ?? 0, ColumnId = "Quantity"
                        });
                values.Add(
                    new CalculationValueModel
                        {
                            RowId = storagePlace.Name,
                            Quantity = quantityAllocated ?? 0,
                            ColumnId = "Allocated"
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
                           new AxisDetailsModel("Description")
                               {
                                   SortOrder = 1, GridDisplayType = GridDisplayType.TextValue
                               },
                           new AxisDetailsModel("Part Number")
                               {
                                   SortOrder = 2, GridDisplayType = GridDisplayType.TextValue
                               },
                           new AxisDetailsModel("Part Description")
                               {
                                   SortOrder = 3, GridDisplayType = GridDisplayType.TextValue
                               },
                           new AxisDetailsModel("UOM")
                               {
                                   SortOrder = 4, GridDisplayType = GridDisplayType.TextValue
                               },
                           new AxisDetailsModel("Raw or Finished")
                               {
                                   SortOrder = 5, GridDisplayType = GridDisplayType.TextValue
                               },
                           new AxisDetailsModel("Quantity") { SortOrder = 6, GridDisplayType = GridDisplayType.Value },
                           new AxisDetailsModel("Allocated") { SortOrder = 7, GridDisplayType = GridDisplayType.Value },
                       };
        }
    }
}