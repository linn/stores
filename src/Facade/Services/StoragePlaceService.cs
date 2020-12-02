namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Facade.Extensions;
    using Linn.Stores.Resources.RequestResources;

    public class StoragePlaceService : IStoragePlaceService
    {
        private readonly IStoragePlaceAuditPack storagePlaceAuditPack;

        private readonly IQueryRepository<StoragePlace> storagePlaceRepository;

        public StoragePlaceService(
            IQueryRepository<StoragePlace> storagePlaceRepository,
            IStoragePlaceAuditPack storagePlaceAuditPack)
        {
            this.storagePlaceAuditPack = storagePlaceAuditPack;
            this.storagePlaceRepository = storagePlaceRepository;
        }

        public IResult<IEnumerable<StoragePlace>> GetStoragePlaces(string searchTerm)
        {
            return new SuccessResult<IEnumerable<StoragePlace>>(
                this.storagePlaceRepository.FilterBy(s => s.Name.Contains(searchTerm.ToUpper())));
        }

        public void CreateAuditReqs(CreateAuditReqsResource resource)
        {
            var employee = resource.Links.FirstOrDefault(a => a.Rel == "created-by");

            if (employee == null)
            {
                throw new InvalidOperationException("Must supply an employee number when creating audit reqs");
            }

            var employeeNumber = employee.Href.ParseId();

            List<StoragePlace> storagePlaces;

            if (!string.IsNullOrEmpty(resource.LocationRange) && resource.LocationRange.StartsWith("E-K"))
            {
                storagePlaces = this.storagePlaceRepository.FilterBy(s => s.Name.StartsWith(resource.LocationRange))
                    .OrderBy(s => s.Name).ToList();
            }
            else
            {
                storagePlaces = this.storagePlaceRepository.FilterBy(s => resource.LocationList.Any(l => s.Name == l))
                    .OrderBy(s => s.Name).ToList();
            }

            foreach (var storagePlace in storagePlaces)
            {
                var result = this.storagePlaceAuditPack.CreateAuditReq(
                    storagePlace.Name,
                    employeeNumber,
                    resource.Department);

                if (result != "SUCCESS")
                {
                    throw new InvalidOperationException(result);
                }
            }
        }
    }
}