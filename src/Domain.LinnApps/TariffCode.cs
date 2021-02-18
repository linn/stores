using Linn.Stores.Domain.LinnApps.Exports;
using System.Collections.Generic;

namespace Linn.Stores.Domain.LinnApps
{
    public class TariffCode
    {
        public int TariffId { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public IEnumerable<ExportReturnDetail> ExportReturnDetails { get; set; }
    }
}
