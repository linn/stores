namespace Linn.Stores.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    public interface IStockQuantitiesService
    {
        IResult<StockQuantities> GetStockQuantities(string partNumber);
    }
}
