namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Tpk;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.Tpk;

    public class TransferableStockListResourceBuilder : IResourceBuilder<IEnumerable<TransferableStock>>
    {
        private readonly TransferableStockResourceBuilder resourceBuilder
            = new TransferableStockResourceBuilder();

        public IEnumerable<TransferableStockResource> Build(IEnumerable<TransferableStock> models)
        {
            return models
                .Select(a => this.resourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<TransferableStock>>.Build(IEnumerable<TransferableStock> models)
            => this.Build(models);

        public string GetLocation(IEnumerable<TransferableStock> models)
        {
            throw new NotImplementedException();
        }
    }
}
