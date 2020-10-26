namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class AuditLocationService : IAuditLocationService
    {
        private readonly IQueryRepository<AuditLocation> repository;

        public AuditLocationService(IQueryRepository<AuditLocation> repository)
        {
            this.repository = repository;
        }

        public IResult<IEnumerable<AuditLocation>> GetAuditLocations(string searchTerm)
        {
            return new SuccessResult<IEnumerable<AuditLocation>>(
                this.repository.FilterBy(a => a.StoragePlace.Contains(searchTerm)));
        }
    }
}