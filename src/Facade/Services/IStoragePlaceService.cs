﻿namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Resources.StockLocators;

    public interface IStoragePlaceService
    {
        IResult<IEnumerable<StoragePlace>> GetStoragePlaces(string searchTerm);

        void CreateAuditReqs(CreateAuditReqsResource resource);

        IResult<StoragePlace> GetStoragePlace(StoragePlaceRequestResource resource);
    }
}
