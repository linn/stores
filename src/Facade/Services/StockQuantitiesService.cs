namespace Linn.Stores.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    public class StockQuantitiesService : IStockQuantitiesService
    {
        private readonly IQueryRepository<StockQuantities> repository;

        public StockQuantitiesService(IQueryRepository<StockQuantities> repository)
        {
            this.repository = repository;
        }

        public IResult<StockQuantities> GetStockQuantities(string partNumber)
        {
            var result = this.repository
                .FindBy(x => x.PartNumber.ToUpper().Equals(partNumber.ToUpper()));
            return new SuccessResult<StockQuantities>(result);
        }
    }
}
