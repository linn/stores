﻿namespace Linn.Stores.Resources.Tpk
{
    using System;
    using System.Collections.Generic;

    public class TpkRequestResource
    {
        public IEnumerable<TransferableStockResource> StockToTransfer { get; set; }

        public DateTime DateTimeTpkViewQueried { get; set; }
    }
}
