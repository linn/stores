namespace Linn.Stores.Domain.LinnApps.Tpk.Models
{
    public class WhatToWandLine
    {
        public int OrderNumber { get; set; }

        public int OrderLine { get; set; }

        public string ArticleNumber { get; set; }

        public string InvoiceDescription { get; set; }

        public string MainsLead { get; set; }

        public int Kitted { get; set; }

        public int Ordered { get; set; }

        public string Sif { get; set; }

        public int ConsignmentId { get; set; }

        public string SerialNumberComments { get; set; }

        public string OldSernos { get; set; }

        public string RenewSernos { get; set; }

        public string Comments { get; set; }
    }
}
