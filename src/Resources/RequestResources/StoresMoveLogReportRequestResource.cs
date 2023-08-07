namespace Linn.Stores.Resources.RequestResources
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class StoresMoveLogReportRequestResource : FromToDateResource
    {
        public string PartNumber { get; set; }

        public string Location { get; set; }

        public string TransType { get; set; }

        public string StockPool { get; set; }
    }
}
