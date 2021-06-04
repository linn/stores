﻿namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;

    public interface IImportBookTransportCodeService
    {
        IResult<IEnumerable<ImportBookTransportCode>> GetTransportCodes();
    }
}
