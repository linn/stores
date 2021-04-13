namespace Linn.Stores.Domain.LinnApps.Tpk.Models
{
    using System.Collections.Generic;

    public class WhatToWandReport
    {
        public IEnumerable<WhatToWandLine> Lines { get; set; }

        public Consignment Consignment { get; set; }

        public string Type { get; set; }

        public SalesAccount Account { get; set; }

        public decimal TotalNettValueOfConsignment { get; set; }
    }
}
