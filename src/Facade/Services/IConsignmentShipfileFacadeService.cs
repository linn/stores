﻿namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;

    public interface IConsignmentShipfileFacadeService
    {
        IResult<IEnumerable<ConsignmentShipfile>> GetShipfiles();
    }
}
