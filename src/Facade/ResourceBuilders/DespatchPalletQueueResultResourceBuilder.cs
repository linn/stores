namespace Linn.Stores.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps.Allocation.Models;
    using Linn.Stores.Resources.Allocation;

    public class DespatchPalletQueueResultResourceBuilder : IResourceBuilder<DespatchPalletQueueResult>
    {
        public DespatchPalletQueueResultResource Build(DespatchPalletQueueResult despatchPalletQueueResult)
        {
            return new DespatchPalletQueueResultResource
                       {
                           TotalNumberOfPallets = despatchPalletQueueResult.TotalNumberOfPallets,
                           NumberOfPalletsToMove = despatchPalletQueueResult.NumberOfPalletsToMove,
                           DespatchPalletQueueDetails = despatchPalletQueueResult.DespatchPalletQueueResultDetails?.Select(
                               a => new DespatchPalletQueueDetailResource
                                        {
                                            KittedFromTime = a.KittedFromTime,
                                            PalletNumber = a.PalletNumber,
                                            PickingSequence = a.PickingSequence,
                                            WarehouseInformation = a.WarehouseInformation,
                                            CanMoveToUpper = a.CanMoveToUpper
                                        }),
                           Links = this.BuildLinks(despatchPalletQueueResult).ToArray()
                       };
        }

        public string GetLocation(DespatchPalletQueueResult despatchPalletQueueResult)
        {
            return "/logistics/allocations/despatch-pallet-queue";
        }

        object IResourceBuilder<DespatchPalletQueueResult>.Build(DespatchPalletQueueResult despatchPalletQueueResult) => this.Build(despatchPalletQueueResult);

        private IEnumerable<LinkResource> BuildLinks(DespatchPalletQueueResult despatchPalletQueueResult)
        {
            yield return new LinkResource { Rel = "self", Href = this.GetLocation(despatchPalletQueueResult) };
        }
    }
}
