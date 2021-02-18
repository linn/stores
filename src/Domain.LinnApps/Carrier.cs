namespace Linn.Stores.Domain.LinnApps
{
    using Linn.Stores.Domain.LinnApps.Exports;
    using System;
    using System.Collections.Generic;

    public class Carrier
    {
        public string CarrierCode { get; set; }

        public string Name { get; set; }

        public int OrganisationId { get; set; }

        public DateTime? DateInvalid { get; set; }

        public IEnumerable<ExportReturn> ExportReturns { get; set; }
    }
}
