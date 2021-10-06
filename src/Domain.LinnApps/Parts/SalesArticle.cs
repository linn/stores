namespace Linn.Stores.Domain.LinnApps.Parts
{
    using System;
    using System.Collections.Generic;

    public class SalesArticle
    {
        public string ArticleNumber { get; set; }

        public string Description { get; set; }

        public Part Part { get; set; }

        public DateTime? PhaseOutDate { get; set; }

        public IEnumerable<Rsn> Rsns { get; set; }

        public Tariff Tariff { get; set; }

        public int TariffId { get; set; }

        public decimal? Weight { get; set; }
    }
}
