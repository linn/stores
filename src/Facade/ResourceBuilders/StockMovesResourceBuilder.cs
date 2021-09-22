namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Resources.StockLocators;

    public class StockMovesResourceBuilder : IResourceBuilder<IEnumerable<StockMove>>
    {
        private readonly StockMoveResourceBuilder resourceBuilder = new StockMoveResourceBuilder();

        public IEnumerable<StockMoveResource> Build(IEnumerable<StockMove> moves)
        {
            return moves
                .Select(a => this.resourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<StockMove>>.Build(IEnumerable<StockMove> moves) 
            => this.Build(moves);

        public string GetLocation(IEnumerable<StockMove> moves)
        {
            throw new NotImplementedException();
        }
    }
}
