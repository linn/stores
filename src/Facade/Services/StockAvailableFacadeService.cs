namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.StockMove.Models;

    public class StockAvailableFacadeService : IStockAvailableFacadeService
    {
        private readonly IQueryRepository<StockAvailable> stockAvailableQueryRepository;

        public StockAvailableFacadeService(IQueryRepository<StockAvailable> stockAvailableQueryRepository)
        {
            this.stockAvailableQueryRepository = stockAvailableQueryRepository;
        }

        public IResult<IEnumerable<StockAvailable>> GetAvailableStock(string partNumber)
        {
            return new SuccessResult<IEnumerable<StockAvailable>>(
                this.stockAvailableQueryRepository.FilterBy(a => a.PartNumber == partNumber));
        }
    }
}
