namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.StockMove.Models;

    public class AvailableStockFacadeService : IAvailableStockFacadeService
    {
        private readonly IQueryRepository<AvailableStock> availableStockQueryRepository;

        public AvailableStockFacadeService(IQueryRepository<AvailableStock> availableStockQueryRepository)
        {
            this.availableStockQueryRepository = availableStockQueryRepository;
        }

        public IResult<IEnumerable<AvailableStock>> GetAvailableStock(string partNumber)
        {
            return new SuccessResult<IEnumerable<AvailableStock>>(
                this.availableStockQueryRepository.FilterBy(a => a.PartNumber == partNumber));
        }
    }
}
