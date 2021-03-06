﻿namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps.Allocation;
    using Linn.Stores.Resources.Allocation;

    public class SosAllocHeadResourceBuilder : IResourceBuilder<SosAllocHead>
    {
        public SosAllocHeadResource Build(SosAllocHead sosAllocHead)
        {
            return new SosAllocHeadResource
                       {
                           AccountId = sosAllocHead.SalesOutlet.AccountId,
                           EarliestRequestedDate = sosAllocHead.EarliestRequestedDate.ToString("o"),
                           JobId = sosAllocHead.JobId,
                           OldestOrder = sosAllocHead.OldestOrder,
                           OutletHoldStatus = sosAllocHead.OutletHoldStatus,
                           OutletNumber = sosAllocHead.SalesOutlet.OutletNumber,
                           ValueToAllocate = sosAllocHead.ValueToAllocate,
                           OutletName = sosAllocHead.SalesOutlet.Name,
                           CountryCode = sosAllocHead.SalesOutlet.CountryCode,
                           CountryName = sosAllocHead.SalesOutlet.CountryName,
                           Links = this.BuildLinks(sosAllocHead).ToArray()
                       };
        }

        public string GetLocation(SosAllocHead sosAllocHead)
        {
            return $"/logistics/sos-alloc-heads/{sosAllocHead.JobId}";
        }

        object IResourceBuilder<SosAllocHead>.Build(SosAllocHead sosAllocHead) => this.Build(sosAllocHead);

        private IEnumerable<LinkResource> BuildLinks(SosAllocHead sosAllocHead)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(sosAllocHead) };
        }
    }
}
