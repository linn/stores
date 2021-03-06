﻿namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;

    public interface IPartDataSheetValuesService
    {
        IResult<IEnumerable<PartDataSheetValues>> GetAll();
    }
}
