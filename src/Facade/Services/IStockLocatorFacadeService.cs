namespace Linn.Stores.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public interface IStockLocatorFacadeService 
        : IFacadeFilterService<StockLocator, int, StockLocatorResource, StockLocatorResource, StockLocatorResource>
    {
        IResult<StockLocator> Delete(int id);
    }
}
