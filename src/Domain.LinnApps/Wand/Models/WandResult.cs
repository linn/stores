namespace Linn.Stores.Domain.LinnApps.Wand.Models
{
    public class WandResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public int ConsignmentId { get; set; }

        public string WandString { get; set; }

        public int? WandLogId { get; set; }

        public WandLog WandLog { get; set; }
    }
}
