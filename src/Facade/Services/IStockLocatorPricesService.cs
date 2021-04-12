namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Resources.StockLocators;

    public interface IStockLocatorPricesService
    {
        IResult<IEnumerable<StockLocatorPrices>> GetPrices(StockLocatorResource queryResource);
    }
}
