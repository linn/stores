namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Resources.StockLocators;

    public class StockMoveResourceBuilder : IResourceBuilder<StockMove>
    {
        public StockMoveResource Build(StockMove move)
        {
            return new StockMoveResource
                       {
                            PartNumber = move.StockLocator.PartNumber,
                            QtyAllocated = move.StockLocator.QuantityAllocated,
                            TransactionCode = move.Header.Lines
                                .FirstOrDefault(l => l.LineNumber == move.LineNumber)
                                ?.TransactionCode,
                            BatchRef = move.StockLocator.BatchRef,
                            ReqNumber = move.ReqNumber,
                            LineNumber = move.LineNumber,
                            Sequence = move.Sequence,
                            DateCreated = move.Header.DateCreated.ToString("o")
                       };
        }

        public string GetLocation(StockMove move)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<StockMove>.Build(StockMove move) =>
            this.Build(move);
    }
}
