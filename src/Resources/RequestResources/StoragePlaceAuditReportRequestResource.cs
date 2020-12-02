namespace Linn.Stores.Resources.RequestResources
{
    using System.Collections.Generic;

    public class StoragePlaceAuditReportRequestResource
    {
        public IEnumerable<string> LocationList { get; set; }

        public string LocationRange { get; set; }
    }
}