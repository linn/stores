namespace Linn.Stores.Domain.LinnApps.StockLocators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Authorisation;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class StockLocatorService : IStockLocatorService
    {
        private readonly IRepository<StockLocator, int> stockLocatorRepository;

        private readonly IStoresPalletRepository palletRepository;

        private readonly IQueryRepository<StoragePlace> storagePlaceRepository;

        private readonly IRepository<StorageLocation, int> storageLocationRepository;

        private readonly IQueryRepository<StockLocatorLocation> stockLocatorLocationsView;

        private readonly IQueryRepository<StockLocatorBatch> stockLocatorBatchesView;

        private readonly IRepository<Part, int> partRepository;

        private readonly IAuthorisationService authService;

        public StockLocatorService(
            IRepository<StockLocator, int> stockLocatorRepository,
            IStoresPalletRepository palletRepository,
            IQueryRepository<StoragePlace> storagePlaceRepository,
            IRepository<StorageLocation, int> storageLocationRepository,
            IQueryRepository<StockLocatorLocation> stockLocatorLocationsView,
            IQueryRepository<StockLocatorBatch> stockLocatorBatchesView,
            IRepository<Part, int> partRepository,
            IAuthorisationService authService)
        {
            this.stockLocatorRepository = stockLocatorRepository;
            this.palletRepository = palletRepository;
            this.storagePlaceRepository = storagePlaceRepository;
            this.storageLocationRepository = storageLocationRepository;
            this.stockLocatorLocationsView = stockLocatorLocationsView;
            this.stockLocatorBatchesView = stockLocatorBatchesView;
            this.partRepository = partRepository;
            this.authService = authService;
        }

        public void UpdateStockLocator(StockLocator @from, StockLocator to, IEnumerable<string> privileges)
        {
            if (!this.authService.HasPermissionFor(AuthorisedAction.UpdateStockLocator, privileges))
            {
                throw new StockLocatorException("You are not authorised to update.");
            }

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

            return toCreate;
        }

        public void DeleteStockLocator(StockLocator toDelete, IEnumerable<string> privileges)
        {
            if (!this.authService.HasPermissionFor(AuthorisedAction.CreateStockLocator, privileges))
            {
                throw new StockLocatorException("You are not authorised to delete.");
            }

            this.stockLocatorRepository.Remove(toDelete);
            if (!this.stockLocatorRepository
                    .FilterBy(l => l.PalletNumber == toDelete.PalletNumber && l.Quantity > 0).Any())
            {
                foreach (var storesPallet in
                    this.palletRepository.FilterBy(p => p.PalletNumber == toDelete.PalletNumber))
                {
                    this.palletRepository.UpdatePallet(storesPallet.PalletNumber, null, null);
                }
            }
        }

        public IEnumerable<StockLocatorWithStoragePlaceInfo> GetStockLocatorsWithStoragePlaceInfoForPart(string partNumber)
        {
            var stockLocators = this.stockLocatorRepository
                .FilterBy(s => s.PartNumber == partNumber);

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
                            auditDept = 
                                this.palletRepository.FindById((int)l.PalletNumber)
                                    .AuditedByDepartmentCode;
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
           var result = (from stockLocator in this.stockLocatorRepository.FilterBy(l => 
                             l.BatchRef.ToUpper().Equals(batchRef.ToUpper()))
                   join storageLocation in this.storageLocationRepository.FindAll() 
                       on stockLocator.LocationId equals
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

        public IEnumerable<StockLocator> GetStockLocatorLocationsView(
            string partNumber,
            string location,
            string stockPool,
            string stockState,
            string batchRef)
        {
            var part = this.partRepository.FindBy(p => p.PartNumber == partNumber);
            if (string.IsNullOrEmpty(batchRef))
            {
                return this.stockLocatorLocationsView.FilterBy(x =>
                        (string.IsNullOrEmpty(partNumber) || x.PartNumber == partNumber)
                        && (string.IsNullOrEmpty(location) || x.StorageLocation.LocationCode == location)
                        && (string.IsNullOrEmpty(stockPool) || x.StockPoolCode == stockPool)
                        && (string.IsNullOrEmpty(stockState) || x.State == stockState))
                    .Select(x => new StockLocator
                                     {
                                         StorageLocation = x.StorageLocation,
                                         PartNumber = x.PartNumber,
                                         Part = part,
                                         Id = x.StorageLocationId,
                                         Quantity = x.Quantity,
                                         PalletNumber = x.PalletNumber,
                                         State = x.State,
                                         QuantityAllocated = x.QuantityAllocated,
                                         StockPoolCode = x.StockPoolCode
                                     });
            }

            return this.stockLocatorBatchesView.FilterBy(x =>
                    (string.IsNullOrEmpty(partNumber) || x.PartNumber == partNumber)
                    && (string.IsNullOrEmpty(stockPool) || x.StockPoolCode == stockPool)
                    && (string.IsNullOrEmpty(location) || x.LocationCode == location)
                    && (string.IsNullOrEmpty(stockState) || x.State == stockState)
                    && x.BatchRef == batchRef)
                .Select(x => new StockLocator
                                 {
                                     PartNumber = x.PartNumber,
                                     Part = part,
                                     Id = x.LocationId,
                                     BatchRef = x.BatchRef,
                                     StockRotationDate = x.StockRotationDate,
                                     Quantity = x.Quantity,
                                     PalletNumber = x.PalletNumber,
                                     State = x.State,
                                     QuantityAllocated = x.QuantityAllocated,
                                     StockPoolCode = x.StockPoolCode
                                 });
        }
    }
}
