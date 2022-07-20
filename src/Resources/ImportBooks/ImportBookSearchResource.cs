namespace Linn.Stores.Resources.ImportBooks
{
    public class ImportBookSearchResource
    {
        public string SearchTerm { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public string CustomsEntryCodePrefix { get; set; }

        public string CustomsEntryCode { get; set; }

        public string CustomsEntryDate { get; set; }

        public int RsnNumber { get; set; }

        public int PoNumber { get; set; }
    }
}
