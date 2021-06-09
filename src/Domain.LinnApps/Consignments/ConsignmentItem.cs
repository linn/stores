﻿namespace Linn.Stores.Domain.LinnApps.Consignments
{
    public class ConsignmentItem
    {
        public int ItemNumber { get; set; }

        public int ConsignmentId { get; set; }

        public int? OrderNumber { get; set; } 

        public SalesOrder SalesOrder { get; set; }

        public Consignment Consignment { get; set; }
    }
}