namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps.Allocation;
    using Linn.Stores.Resources.Allocation;

    public class SosAllocDetailResourceBuilder : IResourceBuilder<SosAllocDetail>
    {
        public SosAllocDetailResource Build(SosAllocDetail sosAllocDetail)
        {
            return new SosAllocDetailResource
                       {
                           Id = sosAllocDetail.Id,
                           AccountId = sosAllocDetail.AccountId,
                           OutletNumber = sosAllocDetail.OutletNumber,
                           DatePossible = sosAllocDetail.DatePossible?.ToString("o"),
                           JobId = sosAllocDetail.JobId,
                           OrderNumber = sosAllocDetail.OrderNumber,
                           OrderLine = sosAllocDetail.OrderLine,
                           ArticleNumber = sosAllocDetail.ArticleNumber,
                           OrderLineHoldStatus = sosAllocDetail.OrderLineHoldStatus,
                           QuantityAllocated = sosAllocDetail.QuantityAllocated,
                           QuantitySuppliable = sosAllocDetail.QuantitySuppliable,
                           QuantityToAllocate = sosAllocDetail.QuantityToAllocate,
                           SupplyInFullCode = sosAllocDetail.SupplyInFullCode,
                           SupplyInFullDate = sosAllocDetail.SupplyInFullDate?.ToString("o"),
                           UnitPriceIncludingVAT = sosAllocDetail.UnitPriceIncludingVAT,
                           Links = this.BuildLinks(sosAllocDetail).ToArray()
                       };
        }

        public string GetLocation(SosAllocDetail sosAllocDetail)
        {
            return $"/logistics/sos-alloc-details/{sosAllocDetail.Id}";
        }

        object IResourceBuilder<SosAllocDetail>.Build(SosAllocDetail sosAllocDetail) => this.Build(sosAllocDetail);

        private IEnumerable<LinkResource> BuildLinks(SosAllocDetail sosAllocDetail)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(sosAllocDetail) };
        }
    }
}
