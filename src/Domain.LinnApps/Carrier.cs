using System;
using System.Collections.Generic;
using System.Text;

namespace Linn.Stores.Domain.LinnApps
{
    public class Carrier
    {
        public string CarrierCode { get; set; }

        public string Name { get; set; }

        public int OrganisationId { get; set; }

        public DateTime? DateInvalid { get; set; }
    }
}
