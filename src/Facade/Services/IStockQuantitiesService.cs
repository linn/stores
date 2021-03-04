namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    public interface IStockQuantitiesService
    {
        IResult<IEnumerable<StockQuantities>> GetStockQuantities(string partNumber);
    }
}
