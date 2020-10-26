namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class StoragePlaceService : IStoragePlaceService
    {
        private readonly IQueryRepository<StoragePlace> repository;

        public StoragePlaceService(IQueryRepository<StoragePlace> repository)
        {
            this.repository = repository;
        }

        public IResult<IEnumerable<StoragePlace>> GetStoragePlaces(string searchTerm)
        {
            return new SuccessResult<IEnumerable<StoragePlace>>(
                this.repository.FilterBy(s => s.StoragePlaceName.Contains(searchTerm)));
        }
    }
}