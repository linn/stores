namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources.StockLocators;

    public class StoragePlaceService : IStoragePlaceService
    {
        private readonly IQueryRepository<StoragePlace> storagePlaceRepository;

        public StoragePlaceService(IQueryRepository<StoragePlace> storagePlaceRepository)
        {
            this.storagePlaceRepository = storagePlaceRepository;
        }

        public IResult<IEnumerable<StoragePlace>> GetStoragePlaces(string searchTerm)
        {
            return new SuccessResult<IEnumerable<StoragePlace>>(
                this.storagePlaceRepository
                    .FilterBy(s => s.Name.Contains(searchTerm.ToUpper()))
                    .ToList()
                    .Take(50));
        }

        public IResult<StoragePlace> GetStoragePlace(StoragePlaceRequestResource resource)
        {
            if (resource.LocationId.HasValue == resource.PalletNumber.HasValue)
            {
                return new BadRequestResult<StoragePlace>("Must supply EITHER Location Id or Pallet Number");
            }

            if (resource.PalletNumber.HasValue)
            {
                return new SuccessResult<StoragePlace>(this
                    .storagePlaceRepository
                    .FindBy(p => p.PalletNumber == resource.PalletNumber));
            } 

            return new SuccessResult<StoragePlace>(this
                    .storagePlaceRepository.FindBy(p => p.LocationId == resource.LocationId));
        }
    }
}
