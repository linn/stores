namespace Linn.Stores.Domain.LinnApps.Parts
{
    using System;

    public class SalesArticle
    {
        public string ArticleNumber { get; set; }

        public string Description { get; set; }

        public DateTime? PhaseOutDate { get; set; }

        public Part Part { get; set; }
    }
}
