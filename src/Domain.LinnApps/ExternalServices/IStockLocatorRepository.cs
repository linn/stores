namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    using System.Linq;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    public interface IStockLocatorRepository : IRepository<StockLocator, int>
    {
        IQueryable<StockLocator> FilterByPartWildcard(string search);
    }
}
