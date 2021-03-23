namespace Linn.Stores.Domain.LinnApps.Requisitions
{
    public class ReqMove
    {
        public int ReqNumber { get; set; }

        public int LineNumber { get; set; }

        public int Sequence { get; set; }

        public int Quantity { get; set; }

        public int? PalletNumber { get; set; }
    }
}
