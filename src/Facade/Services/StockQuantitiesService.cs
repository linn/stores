namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    public class StockQuantitiesService : IStockQuantitiesService
    {
        private readonly IFilterByWildcardQueryRepository<StockQuantities> repository;

        public StockQuantitiesService(IFilterByWildcardQueryRepository<StockQuantities> repository)
        {
            this.repository = repository;
        }

        public IResult<IEnumerable<StockQuantities>> GetStockQuantities(string partNumber)
        {
            var result = this.repository.FilterByWildcard(partNumber.Replace("*", "%").ToUpper());
            return new SuccessResult<IEnumerable<StockQuantities>>(result);
        }
    }
}
