namespace Linn.Stores.Domain.LinnApps.Tpk.Models
{
    using System;
    using System.Collections.Generic;

    public class TpkRequest
    {
        public IEnumerable<TransferableStock> StockToTransfer { get; set; }

        public DateTime DateTimeTpkViewQueried { get; set; }
    }
}
