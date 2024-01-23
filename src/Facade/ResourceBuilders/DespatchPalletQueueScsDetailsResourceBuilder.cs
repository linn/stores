namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Stores.Domain.LinnApps.Allocation;
    using Linn.Stores.Resources.Allocation;

    public class DespatchPalletQueueScsDetailsResourceBuilder : IResourceBuilder<IEnumerable<DespatchPalletQueueScsDetail>>
    {
        private readonly DespatchPalletQueueScsDetailResourceBuilder despatchPalletQueueScsDetailResourceBuilder = new DespatchPalletQueueScsDetailResourceBuilder();

        public IEnumerable<DespatchPalletQueueScsDetailResource> Build(IEnumerable<DespatchPalletQueueScsDetail> queueScsDetails)
        {
            return queueScsDetails
                .Select(a => this.despatchPalletQueueScsDetailResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<DespatchPalletQueueScsDetail>>.Build(IEnumerable<DespatchPalletQueueScsDetail> queueScsDetails) => this.Build(queueScsDetails);

        public string GetLocation(IEnumerable<DespatchPalletQueueScsDetail> queueScsDetails)
        {
            throw new NotImplementedException();
        }
    }
}
