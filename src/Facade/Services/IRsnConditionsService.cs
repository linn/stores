namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.GoodsIn;

    public interface IRsnConditionsService
    {
        IResult<IEnumerable<RsnCondition>> GetRsnConditions();
    }
}
