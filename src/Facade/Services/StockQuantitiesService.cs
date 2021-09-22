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
            if (!partNumber.Contains("*"))
            {
                var res = this.repository
                    .FilterBy(x => x.PartNumber == partNumber.ToUpper());
                return new SuccessResult<IEnumerable<StockQuantities>>(res);
            }

            var partNumberPattern = Regex.Escape(partNumber.Trim(' ')).Replace("\\*", ".*?");
            var r = new Regex(partNumberPattern, RegexOptions.IgnoreCase);

            var result = this.repository
                .FilterBy(x => r.IsMatch(x.PartNumber));
            return new SuccessResult<IEnumerable<StockQuantities>>(result);
        }
    }
}
