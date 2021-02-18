using Linn.Stores.Domain.LinnApps.Exports;
using System.Collections.Generic;

namespace Linn.Stores.Domain.LinnApps
{
    public class Hub
    {
        public int HubId { get; set; }

        public string Description { get; set; }

        public string CarrierCode { get; set; }

        public IEnumerable<ExportReturn> ExportReturns { get; set; }
    }
}
