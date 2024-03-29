﻿namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;

    public interface IExportRsnService
    {
        IResult<IEnumerable<ExportRsn>> SearchRsns(int accountId, int? outletNumber, string searchTerm, string hasExportReturn);
    }
}