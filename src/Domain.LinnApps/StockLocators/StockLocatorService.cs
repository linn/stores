namespace Linn.Stores.Domain.LinnApps.StockLocators
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Authorisation;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Exceptions;

    public class StockLocatorService : IStockLocatorService
    {
        private readonly IRepository<StockLocator, int> repository;

        private readonly IStoresPalletRepository palletRepository;

        private readonly IQueryRepository<StoragePlace> storagePlaceRepository;

        private readonly IAuthorisationService authService;

        public StockLocatorService(
            IRepository<StockLocator, int> repository,
            IStoresPalletRepository palletRepository,
            IQueryRepository<StoragePlace> storagePlaceRepository,
            IAuthorisationService authService)
        {
            this.repository = repository;
            this.palletRepository = palletRepository;
            this.storagePlaceRepository = storagePlaceRepository;
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

            this.repository.Remove(toDelete);
            if (!this.repository
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
            var stockLocators = this.repository
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
    }
}
