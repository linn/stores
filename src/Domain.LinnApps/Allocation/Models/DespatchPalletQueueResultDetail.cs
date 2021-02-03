namespace Linn.Stores.Domain.LinnApps.Allocation.Models
{
    public class DespatchPalletQueueResultDetail
    {
        public int PalletNumber { get; set; }

        public string KittedFromTime { get; set; }

        public int PickingSequence { get; set; }

        public string WarehouseInformation { get; set; }

        public bool CanMoveToUpper { get; set; }
    }
}
