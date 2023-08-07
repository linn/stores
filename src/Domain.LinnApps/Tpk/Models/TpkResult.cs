namespace Linn.Stores.Domain.LinnApps.Tpk.Models
{
    using System.Collections.Generic;

    public class TpkResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public IEnumerable<TransferredStock> Transferred { get; set; }

        public IEnumerable<WhatToWandConsignment> Consignments { get; set; }
    }
}
