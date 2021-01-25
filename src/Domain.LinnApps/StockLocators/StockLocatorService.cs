﻿namespace Linn.Stores.Domain.LinnApps.StockLocators
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Exceptions;

    public class StockLocatorService : IStockLocatorService
    {
        private readonly IRepository<StockLocator, int> repository;

        private readonly IRepository<StoresPallet, int> palletRepository;

        private readonly IQueryRepository<StoragePlace> storagePlaceRepository;

        public StockLocatorService(
            IRepository<StockLocator, int> repository,
            IRepository<StoresPallet, int> palletRepository,
            IQueryRepository<StoragePlace> storagePlaceRepository)
        {
            this.repository = repository;
            this.palletRepository = palletRepository;
            this.storagePlaceRepository = storagePlaceRepository;
        }

        public void UpdateStockLocator(StockLocator @from, StockLocator to)
        {
            throw new System.NotImplementedException();
        }

        public StockLocator CreateStockLocator(StockLocator toCreate, string auditDepartmentCode)
        {
            if (toCreate.LocationId.HasValue == toCreate.PalletNumber.HasValue)
            {
                throw new CreateStockLocatorException("Must Supply EITHER Location Id OR Pallet Number");
            }

            if (toCreate.PalletNumber != null)
            {
                var pallets = this.palletRepository.FilterBy(p => p.PalletNumber == toCreate.PalletNumber);
                foreach (var storesPallet in pallets)
                {
                    if (storesPallet.AuditedByDepartmentCode == null && auditDepartmentCode == null)
                    {
                        throw new CreateStockLocatorException("Audit department must be entered");
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

        public void DeleteStockLocator(StockLocator toDelete)
        {
            this.repository.Remove(toDelete);
            if (!this.repository
                    .FilterBy(l => l.PalletNumber == toDelete.PalletNumber && l.Quantity > 0).Any())
            {
                return;
            }

            foreach (var storesPallet in 
                this.palletRepository.FilterBy(p => p.PalletNumber == toDelete.PalletNumber))
            {
                storesPallet.AuditFrequencyWeeks = null;
                storesPallet.AuditedByDepartmentCode = null;
            }
        }

        public IEnumerable<StockLocatorWithStoragePlaceInfo> GetStockLocatorsWithStoragePlaceInfoForPart(string partNumber)
        {
            var stockLocators = this.repository
                .FilterBy(s => s.PartNumber == partNumber);

            return stockLocators.ToList().Select(
                s =>
                    {
                        var l = this.storagePlaceRepository.FindBy(
                            p => s.PalletNumber == null
                                     ? p.LocationId == s.LocationId
                                     : s.PalletNumber == p.PalletNumber);
                        return new StockLocatorWithStoragePlaceInfo
                                   {
                                       Id = s.Id,
                                       StoragePlaceDescription = l.Description,
                                       StoragePlaceName = l.Name,
                                       PartNumber = s.PartNumber,
                                       BatchRef = s.BatchRef,
                                       Quantity = s.Quantity,
                                       StockRotationDate = s.StockRotationDate,
                                       Remarks = s.Remarks
                                   };
                    });
        }
    }
}
