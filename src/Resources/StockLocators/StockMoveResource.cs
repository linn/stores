namespace Linn.Stores.Resources.StockLocators
{
    public class StockMoveResource
    {
        public string PartNumber { get; set; }

        public decimal? QtyAllocated { get; set; }

        public string TransactionCode { get; set; }

        public string BatchRef { get; set; }

        public int ReqNumber { get; set; }

        public int LineNumber { get; set; }

        public int Sequence { get; set; }

        public string DateCreated { get; set; }
    }
}
