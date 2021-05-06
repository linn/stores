﻿namespace Linn.Stores.Domain.LinnApps
{
    using System;
    using System.Collections.Generic;

    public class SalesAccount
    {
        public int AccountId { get; set; }

        public string AccountName { get; set; }

        public string AccountType { get; set; }

        public DateTime? DateClosed { get; set; }

        public int? OrgId { get; set; }

        public int? ContactId { get; set; }

        public IEnumerable<SalesOrder> SalesOrders { get; set; }

        public Contact ContactDetails { get; set; }
    }
}
