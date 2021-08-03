namespace Linn.Stores.Resources.Purchasing
{
    public class PlCreditDebitNoteResource
    {
        public int NoteNumber { get; set; }

        public string NoteType { get; set; }

        public string PartNumber { get; set; }

        public int OrderQty { get; set; }

        public int? OriginalOrderNumber { get; set; }

        public int? ReturnsOrderNumber { get; set; }

        public decimal NetTotal { get; set; }

        public string Notes { get; set; }

        public string DateClosed { get; set; }

        public int? ClosedBy { get; set; }

        public string ReasonClosed { get; set; }
    }
}
