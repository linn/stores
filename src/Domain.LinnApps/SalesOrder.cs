﻿namespace Linn.Stores.Domain.LinnApps
{
    using System.Collections.Generic;

    using Linn.Stores.Domain.LinnApps.Consignments;

    public class SalesOrder
    {
        public int OrderNumber { get; set; }

        public int AccountId { get; set; }

        public SalesAccount Account { get; set; }

        public string CurrencyCode { get; set; }

        public int OutletNumber { get; set; }

        public SalesOutlet SalesOutlet { get; set; }

        public IEnumerable<ConsignmentItem> ConsignmentItems { get; set; }
    }
}
