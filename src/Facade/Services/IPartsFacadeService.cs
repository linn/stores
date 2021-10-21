﻿namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    public interface IPartsFacadeService : IFacadeService<Part, int, PartResource, PartResource>
    {
        IResult<IEnumerable<Part>> GetDeptStockPalletParts();

        void CreatePartFromSource(int sourceId, int proposedById, IEnumerable<PartDataSheetResource> dataSheets);

        IResult<IEnumerable<Part>> GetPartByPartNumber(string partNumber);

        IResult<Part> GetByIdNoTracking(int id);
    }
}
