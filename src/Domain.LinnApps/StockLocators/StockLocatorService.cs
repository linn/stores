﻿namespace Linn.Stores.Domain.LinnApps.StockLocators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Authorisation;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class StockLocatorService : IStockLocatorService
    {
        private readonly IRepository<StockLocator, int> stockLocatorRepository;

        private readonly IStoresPalletRepository palletRepository;

        private readonly IQueryRepository<StoragePlace> storagePlaceRepository;

        private readonly IRepository<StorageLocation, int> storageLocationRepository;

        private readonly IQueryRepository<StockLocatorBatch> stockLocatorBatchesView;

        private readonly IQueryRepository<StockLocatorPrices> stockLocatorView;

        private readonly IAuthorisationService authService;

        private readonly IStockLocatorLocationsViewService locationsViewService;

        public StockLocatorService(
            IRepository<StockLocator, int> stockLocatorRepository,
            IStoresPalletRepository palletRepository,
            IQueryRepository<StoragePlace> storagePlaceRepository,
            IRepository<StorageLocation, int> storageLocationRepository,
            IQueryRepository<StockLocatorBatch> stockLocatorBatchesView,
            IAuthorisationService authService,
            IStockLocatorLocationsViewService locationsViewService,
            IQueryRepository<StockLocatorPrices> stockLocatorView)
        {
            this.stockLocatorRepository = stockLocatorRepository;
            this.palletRepository = palletRepository;
            this.storagePlaceRepository = storagePlaceRepository;
            this.storageLocationRepository = storageLocationRepository;
            this.stockLocatorBatchesView = stockLocatorBatchesView;
            this.authService = authService;
            this.locationsViewService = locationsViewService;
            this.stockLocatorView = stockLocatorView;
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

        public IEnumerable<StockLocatorWithStoragePlaceInfo> GetStockLocatorsWithStoragePlaceInfoForPart(int partId)
        {
            var stockLocators = this.stockLocatorRepository
                .FilterBy(s => s.Part.Id == partId);

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

        public IEnumerable<StockLocator> SearchStockLocators(
            string partNumber,
            int? locationId,
            int? palletNumber,
            string stockPool,
            string stockState,
            string category)
        {
            return this.locationsViewService.QueryView(
                partNumber?.Trim(' '),
                locationId, 
                palletNumber,
                stockPool,
                stockState, 
                category).Select(x => new StockLocator
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
                Part = new Part { PartNumber = x.PartNumber, OurUnitOfMeasure = x.OurUnitOfMeasure}
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
            return this
                .stockLocatorBatchesView
                .FilterBy(x => (locationId == null || x.LocationId == locationId)
                        && (palletNumber == null || x.PalletNumber == palletNumber)
                        && (string.IsNullOrEmpty(partNumber) || x.PartNumber == partNumberTrimmed)
                        && (string.IsNullOrEmpty(stockPool) || x.StockPoolCode == stockPool)
                        && (string.IsNullOrEmpty(category) || x.Category == category)
                        && (string.IsNullOrEmpty(stockState) || x.State == stockState))
                .Select(x => new StockLocator
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
            int? locationId,
            string state,
            string category,
            string stockPool,
            string batchRef,
            DateTime? batchDate)
        {
            return this.stockLocatorView.FilterBy(x => false);
        }
    }
}
