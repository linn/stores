namespace Linn.Stores.Domain.LinnApps.Parts
{
    using System;

    public class QcControl
    {
        public int? Id { get; set; }

        public string PartNumber { get; set; }

        public DateTime TransactionDate { get; set; }

        public int? ChangedBy { get; set; }

        public int NumberOfBookIns { get; set; }

        public int NumberOfBookInsDone { get; set; }

        public string Reason { get; set; }

        public string OnOrOffQc { get; set; }
    }
}
