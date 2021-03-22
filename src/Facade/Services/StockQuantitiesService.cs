namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

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

        public IResult<IEnumerable<StockQuantities>> GetStockQuantities(string partNumber)
        {
            var partNumberPattern = Regex.Escape(partNumber.Trim(' ')).Replace("\\*", ".*?");
            var r = new Regex(partNumberPattern, RegexOptions.IgnoreCase);

            var result = this.repository
                .FilterBy(x => r.IsMatch(x.PartNumber));
            return new SuccessResult<IEnumerable<StockQuantities>>(result);
        }
    }
}
