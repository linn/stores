namespace Linn.Stores.Resources.Parts
{
    using Linn.Common.Resources;

    public class ImportBookPostEntryResource : HypermediaResource
    {
        public int ImportBookId { get; set; }

        public int LineNumber { get; set; }

        public string EntryCodePrefix { get; set; }

        public string EntryCode { get; set; }

        public string EntryDate { get; set; }

        public string Reference { get; set; }

        public int? Duty { get; set; }

        public int? Vat { get; set; }
    }
}
