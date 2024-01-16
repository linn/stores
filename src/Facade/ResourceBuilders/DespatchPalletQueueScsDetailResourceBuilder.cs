namespace Linn.Stores.Facade.ResourceBuilders
{

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Allocation;
    using Linn.Stores.Resources.Allocation;

    public class DespatchPalletQueueScsDetailResourceBuilder : IResourceBuilder<DespatchPalletQueueScsDetail>
    {
        public DespatchPalletQueueScsDetailResource Build(DespatchPalletQueueScsDetail despatchPalletQueueDetail)
        {
            return new DespatchPalletQueueScsDetailResource
            {
                PalletNumber = despatchPalletQueueDetail.PalletNumber,
                KittedFromTime = despatchPalletQueueDetail.KittedFromTime,
                PickingSequence = despatchPalletQueueDetail.PickingSequence
            };
        }

        public string GetLocation(DespatchPalletQueueScsDetail despatchPalletQueueDetail)
        {
            return "/logistics/allocations/despatch-pallet-queue/scs";
        }

        object IResourceBuilder<DespatchPalletQueueScsDetail>.Build(DespatchPalletQueueScsDetail despatchPalletQueueDetail) => this.Build(despatchPalletQueueDetail);
    }
}
