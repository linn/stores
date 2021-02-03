namespace Linn.Stores.Domain.LinnApps.Allocation.Models
{
    public class DespatchPalletQueueDetail
    {
        public int PalletNumber { get; set; }

        public string KittedFrom { get; set; }

        public int PickingSequence { get; set; }

        public string WarehouseInformation { get; set; }
    }
}
