namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Tpk;

    public class TransferableStockListResponseProcessor : JsonResponseProcessor<IEnumerable<TransferableStock>>
    {
        public TransferableStockListResponseProcessor(IResourceBuilder<IEnumerable<TransferableStock>> resourceBuilder)
            : base(resourceBuilder, "transferable-stock", 1)
        {
        }
    }
}
