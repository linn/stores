namespace Linn.Stores.Domain.LinnApps.StockLocators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Authorisation;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Domain.LinnApps.Requisitions;

    public class StockLocatorService : IStockLocatorService
    {
        private readonly IFilterByWildcardRepository<StockLocator, int> stockLocatorRepository;

        private readonly IStoresPalletRepository palletRepository;

        private readonly IQueryRepository<StoragePlace> storagePlaceRepository;

        private readonly IRepository<StorageLocation, int> storageLocationRepository;

        private readonly IQueryRepository<StockLocatorBatch> stockLocatorBatchesView;

        private readonly IQueryRepository<StockLocatorPrices> stockLocatorView;

        private readonly IAuthorisationService authService;

        private readonly IStockLocatorLocationsViewService locationsViewService;

        private readonly IPartRepository partRepository;

        private readonly IRepository<ReqMove, ReqMoveKey> reqMoveRepository;

        private readonly IQueryRepository<StockTriggerLevel> triggerLevelRepository;

        public StockLocatorService(
            IFilterByWildcardRepository<StockLocator, int> stockLocatorRepository,
            IStoresPalletRepository palletRepository,
            IQueryRepository<StoragePlace> storagePlaceRepository,
            IRepository<StorageLocation, int> storageLocationRepository,
            IQueryRepository<StockLocatorBatch> stockLocatorBatchesView,
            IAuthorisationService authService,
            IStockLocatorLocationsViewService locationsViewService,
            IQueryRepository<StockLocatorPrices> stockLocatorView,
            IPartRepository partRepository,
            IRepository<ReqMove, ReqMoveKey> reqMoveRepository,
            IQueryRepository<StockTriggerLevel> triggerLevelRepository)
        {
            this.stockLocatorRepository = stockLocatorRepository;
            this.palletRepository = palletRepository;
            this.storagePlaceRepository = storagePlaceRepository;
            this.storageLocationRepository = storageLocationRepository;
            this.stockLocatorBatchesView = stockLocatorBatchesView;
            this.authService = authService;
            this.locationsViewService = locationsViewService;
            this.stockLocatorView = stockLocatorView;
            this.partRepository = partRepository;
            this.reqMoveRepository = reqMoveRepository;
            this.triggerLevelRepository = triggerLevelRepository;
        }

        public void UpdateStockLocator(StockLocator from, StockLocator to, IEnumerable<string> privileges)
        {
            if (!this.authService.HasPermissionFor(AuthorisedAction.UpdateStockLocator, privileges))
            {
                throw new StockLocatorException("You are not authorised to update.");
            }

            from.Part = this.partRepository.FindBy(p => p.PartNumber == from.PartNumber);

            from.BatchRef = to.BatchRef;
            from.StockRotationDate = to.StockRotationDate;
            from.Quantity = to.Quantity;
            from.Remarks = to.Remarks;
            from.PalletNumber = to.PalletNumber;
            from.LocationId = to.LocationId;
        }

        public StockLocator CreateStockLocator(
            StockLocator toCreate,
            string auditDepartmentCode,
            IEnumerable<string> privileges)
        {
            if (!this.authService.HasPermissionFor(AuthorisedAction.CreateStockLocator, privileges))
            {
                throw new StockLocatorException("You are not authorised to create.");
            }

            if (toCreate.LocationId.HasValue == toCreate.PalletNumber.HasValue)
            {
                throw new StockLocatorException("Must Supply EITHER Location Id OR Pallet Number");
            }

            if (toCreate.PalletNumber != null)
            {
                var pallets = this.palletRepository.FilterBy(p => p.PalletNumber == toCreate.PalletNumber);
                foreach (var storesPallet in pallets)
                {
                    if (storesPallet.AuditedByDepartmentCode == null && auditDepartmentCode == null)
                    {
                        throw new StockLocatorException("Audit department must be entered");
                    }

                    if (auditDepartmentCode != null)
                    {
                        storesPallet.AuditFrequencyWeeks = 26;
                        storesPallet.AuditedByDepartmentCode = auditDepartmentCode;
                    }
                }
            }

            toCreate.StockPoolCode = "LINN DEPT";
            toCreate.State = "STORES";
            toCreate.Category = "FREE";

            if (!toCreate.Quantity.HasValue || toCreate.Quantity == 0)
            {
                toCreate.Quantity = 1;
            }

            toCreate.Part = this.partRepository.FindBy(p => p.PartNumber == toCreate.PartNumber);

            return toCreate;
        }

        public void DeleteStockLocator(StockLocator toDelete, IEnumerable<string> privileges)
        {
            if (!this.authService.HasPermissionFor(AuthorisedAction.UpdateStockLocator, privileges))
            {
                throw new StockLocatorException("You are not authorised to delete.");
            }

            foreach (var dependentReqMove 
                in this.reqMoveRepository.FilterBy(m => m.StockLocatorId == toDelete.Id))
            {
                dependentReqMove.Remarks =
                    $"Referenced stock locator for departmental stock {toDelete.PartNumber} from Pallet {toDelete.PalletNumber} deleted on {DateTime.Today.ToShortDateString()}";
                dependentReqMove.StockLocatorId = null;
                dependentReqMove.StockLocator = null;
            }

            this.stockLocatorRepository.Remove(toDelete);

            toDelete.Part = this.partRepository.FindBy(p => p.PartNumber == toDelete.PartNumber);

            if (!this.stockLocatorRepository.FilterBy(l => l.PalletNumber == toDelete.PalletNumber && l.Quantity > 0)
                    .Any())
            {
                foreach (var storesPallet in this.palletRepository.FilterBy(
                    p => p.PalletNumber == toDelete.PalletNumber))
                {
                    this.palletRepository.UpdatePallet(storesPallet.PalletNumber, null, null);
                }
            }
        }

        public IEnumerable<StockLocatorWithStoragePlaceInfo> GetStockLocatorsWithStoragePlaceInfoForPart(int partId)
        {
            var stockLocators = this.stockLocatorRepository.FilterBy(s => s.Part.Id == partId);

            string auditDept = string.Empty;

            return stockLocators.ToList().Select(
                s =>
                    {
                        var l = this.storagePlaceRepository.FindBy(
                            p => s.PalletNumber == null
                                     ? p.LocationId == s.LocationId
                                     : s.PalletNumber == p.PalletNumber);
                        if (l.PalletNumber.HasValue)
                        {
                            auditDept = this.palletRepository.FindById((int)l.PalletNumber).AuditedByDepartmentCode;
                        }

                        return new StockLocatorWithStoragePlaceInfo
                                   {
                                       Id = s.Id,
                                       StoragePlaceDescription = l.Description,
                                       StoragePlaceName = l.Name,
                                       PartNumber = s.PartNumber,
                                       PalletNumber = s.PalletNumber,
                                       LocationId = s.LocationId,
                                       BatchRef = s.BatchRef,
                                       Quantity = s.Quantity,
                                       StockRotationDate = s.StockRotationDate,
                                       Remarks = s.Remarks,
                                       AuditDepartmentCode = auditDept
                                   };
                    }).OrderBy(p => p.PalletNumber);
        }

        public IEnumerable<StockLocator> GetBatches(string batchRef)
        {
            var result =
                (from stockLocator in
                     this.stockLocatorRepository.FilterBy(l => l.BatchRef.ToUpper().Equals(batchRef.ToUpper()))
                 join storageLocation in this.storageLocationRepository.FindAll() on stockLocator.LocationId equals
                     storageLocation.LocationId into gj
                 from storageLocation in gj.DefaultIfEmpty()
                 select new StockLocator
                            {
                                BatchRef = stockLocator.BatchRef,
                                StockRotationDate = stockLocator.StockRotationDate,
                                PartNumber = stockLocator.PartNumber,
                                PalletNumber = stockLocator.PalletNumber,
                                Id = stockLocator.Id,
                                Category = stockLocator.Category,
                                State = stockLocator.State,
                                StorageLocation = new StorageLocation
                                                      {
                                                          LocationCode = storageLocation.LocationCode,
                                                          Description = storageLocation.Description
                                                      }
                            }).AsEnumerable().Distinct(new StockLocatorEquals());
            return result;
        }

        public IEnumerable<StockLocator> SearchStockLocators(
            string partNumber,
            int? locationId,
            int? palletNumber,
            string stockPool,
            string stockState,
            string category,
            string locationName,
            string partDescription)
        {
            return this.locationsViewService
                .QueryView(
                    partNumber?.Trim(' '), 
                    locationId, 
                    palletNumber, 
                    stockPool, 
                    stockState, 
                    category, 
                    locationName,
                    partDescription)
                .Select(
                    x => new StockLocator
                             {
                                 PartNumber = x.PartNumber,
                                 Id = x.StorageLocationId,
                                 LocationId = x.StorageLocationId,
                                 StorageLocation = x.StorageLocation,
                                 Quantity = x.Quantity,
                                 PalletNumber = x.PalletNumber,
                                 State = x.State,
                                 QuantityAllocated = x.QuantityAllocated,
                                 StockPoolCode = x.StockPoolCode,
                                 Part = new Part 
                                            { 
                                                PartNumber = x.PartNumber, 
                                                OurUnitOfMeasure = x.OurUnitOfMeasure, 
                                                Description = x.PartDescription, 
                                                Id = x.Part.Id
                                            },
                                 TriggerLevel = x.PalletNumber.HasValue 
                                                    ? this.triggerLevelRepository
                                                        .FindBy(l => l.PartNumber.Equals(x.PartNumber)
                                                                     && l.PalletNumber.Equals(x.PalletNumber))
                                                    : this.triggerLevelRepository
                                                    .FindBy(l => l.PartNumber.Equals(x.PartNumber)
                                                                 && l.LocationId.Equals(x.StorageLocationId))
                    });
        }

        public IEnumerable<StockLocator> SearchStockLocatorBatchView(
            string partNumber,
            int? locationId,
            int? palletNumber,
            string stockPool,
            string stockState,
            string category)
        {
            var partNumberTrimmed = partNumber?.Trim(' ');
            return this.stockLocatorBatchesView
                .FilterBy(
                    x => (locationId == null || x.LocationId == locationId)
                         && (palletNumber == null || x.PalletNumber == palletNumber)
                         && (string.IsNullOrEmpty(partNumber) || x.PartNumber == partNumberTrimmed)
                         && (string.IsNullOrEmpty(stockPool) || x.StockPoolCode == stockPool)
                         && (string.IsNullOrEmpty(category) || x.Category == category)
                         && (string.IsNullOrEmpty(stockState) || x.State == stockState))
                .Select(
                    x => new StockLocator
                             {
                                 PartNumber = x.PartNumber,
                                 Id = x.LocationId,
                                 LocationId = x.LocationId,
                                 BatchRef = x.BatchRef,
                                 StorageLocation = new StorageLocation { LocationCode = x.LocationCode },
                                 StockRotationDate = x.StockRotationDate,
                                 Quantity = x.Quantity,
                                 PalletNumber = x.PalletNumber,
                                 State = x.State,
                                 QuantityAllocated = x.QuantityAllocated,
                                 StockPoolCode = x.StockPoolCode,
                                 Category = x.Category
                             });
        }

        public IEnumerable<StockLocatorPrices> GetPrices(
            int? palletNumber,
            string partNumber,
            string locationCode,
            string state,
            string category,
            string stockPool,
            string batchRef,
            DateTime? batchDate)
        {
            return this.stockLocatorView.FilterBy(
                x => (string.IsNullOrEmpty(locationCode) || x.LocationCode == locationCode)
                     && (palletNumber == null || x.Pallet == palletNumber) 
                     && (string.IsNullOrEmpty(partNumber) || x.PartNumber == partNumber) 
                     && (string.IsNullOrEmpty(stockPool) || x.StockPool == stockPool) 
                     && (string.IsNullOrEmpty(category) || x.Category == category)
                     && (string.IsNullOrEmpty(batchRef) || x.BatchRef == batchRef)
                     && (batchDate == null || x.BatchDate.Value.Date.Equals(batchDate.Value.Date))
                     && (string.IsNullOrEmpty(state) || x.State == state));
        }

        public IEnumerable<StockMove> GetMoves(string partNumber, int? palletNumber, int? locationId)
        {
            IEnumerable<int> locators;

            if (palletNumber != null)
            {
                locators = this.stockLocatorRepository.FilterBy(l =>
                        l.PartNumber == partNumber
                        && (l.PalletNumber == palletNumber)
                        && l.QuantityAllocated > 0)
                    .Select(l => l.Id).ToList();
            }
            else
            {
                locators = this.stockLocatorRepository.FilterBy(l =>
                        l.PartNumber == partNumber
                        && l.LocationId == locationId
                        && l.QuantityAllocated > 0)
                    .Select(l => l.Id).ToList();
            }
            
            return this.reqMoveRepository.FilterBy(m 
                => m.StockLocatorId.HasValue 
                   && locators.Contains((int)m.StockLocatorId)
                   && m.DateBooked == null
                   && m.DateCancelled == null)
                .Select(m => new StockMove(m)).ToList();
        }

        public IEnumerable<StockLocator> GetBatchesInRotationOrderByPart(string partSearch)
        {
            var stockLocators = this.stockLocatorRepository.FilterByWildcard(partSearch.Replace("*", "%"))
                .Where(l => l.Quantity > 0);

            var parts = stockLocators.Select(s => s.PartNumber).Distinct();

            if (parts.Count() > 100)
            {
                throw new StockLocatorException("Too many results for the report to handle. Please refine your Part Number search");
            }

            return stockLocators.GroupBy(
                x => new
                         {
                             x.Id,
                             x.PartNumber,
                             x.BatchRef,
                             x.StockRotationDate,
                             x.StockPoolCode,
                             x.State,
                             IsPallet = x.PalletNumber.HasValue,
                             Loc = x.PalletNumber ?? x.LocationId
                         }).Select(
                g => new StockLocator
                         {
                             Id = g.Key.Id,
                             PartNumber = g.Key.PartNumber,
                             Part = stockLocators.SingleOrDefault(x => x.Id == g.Key.Id).Part,
                             Quantity = g.Sum(e => e.Quantity ?? 0),
                             QuantityAllocated = g.Sum(e => e.QuantityAllocated ?? 0),
                             BatchRef = g.Key.BatchRef,
                             StockRotationDate = g.Key.StockRotationDate,
                             StockPoolCode = g.Key.StockPoolCode,
                             State = g.Key.State,
                             PalletNumber = g.Key.IsPallet ? g.Key.Loc : (int?)null,
                             StorageLocation = !g.Key.IsPallet
                                                   ? stockLocators.SingleOrDefault(l => l.Id == g.Key.Id)
                                                       .StorageLocation
                                                   : null
                         })
                .OrderBy(s => s.StockPoolCode)
                .ThenBy(s => s.State)
                .ThenBy(s => s.StockRotationDate)
                .ThenBy(s => s.Id);
        }
    }
}
