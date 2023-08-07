namespace Linn.Stores.Domain.LinnApps.Tqms
{
    using System;

    public class TqmsCategory
    {
        public string Category { get; set; }

        public string Description { get; set; }

        public int? SortOrder { get; set; }

        public string Explanation { get; set; }

        public DateTime? DateInvalid { get; set; }
    }
}
