namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockMove.Models;

    public interface IStockAvailableFacadeService
    {
        IResult<IEnumerable<StockAvailable>> GetAvailableStock(string partNumber);
    }
}
