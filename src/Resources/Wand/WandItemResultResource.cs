namespace Linn.Stores.Resources.Wand
{
    public class WandItemResultResource
    {
        public string Message { get; set; }

        public bool Success { get; set; }

        public int ConsignmentId { get; set; }

        public string WandString { get; set; }

        public WandLogResource WandLog { get; set; }
    }
}
