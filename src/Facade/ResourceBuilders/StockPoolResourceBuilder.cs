namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources.StockLocators;

    public class StockPoolResourceBuilder : IResourceBuilder<StockPool>
    {
        public StockPoolResource Build(StockPool stockPool)
        {
            return new StockPoolResource
                       {
                           Id = stockPool.Id,
                           Sequence = stockPool.Sequence,
                           StockPoolCode = stockPool.StockPoolCode,
                           AccountingCompany = stockPool.AccountingCompany,
                           DefaultLocation = stockPool.DefaultLocation,
                           Description = stockPool.Description,
                           StockCategory = stockPool.StockCategory,
                           DateInvalid = stockPool.DateInvalid?.ToString("o"),
                           Links = this.BuildLinks(stockPool).ToArray()
                       };
        }

        public string GetLocation(StockPool stockPool)
        {
            return $"/inventory/stock-pools/{stockPool.Id}";
        }

        object IResourceBuilder<StockPool>.Build(StockPool stockPool) => this.Build(stockPool);

        private IEnumerable<LinkResource> BuildLinks(StockPool stockPool)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(stockPool) };
        }
    }
}
