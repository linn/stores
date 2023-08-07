namespace Linn.Stores.Resources.Tpk
{
    using System.Collections.Generic;

    public class WhatToWandConsignmentResource
    {
        public IEnumerable<WhatToWandLineResource> Lines { get; set; }

        public TpkConsignmentResource Consignment { get; set; }

        public string Type { get; set; }

        public SalesAccountResource Account { get; set; }
    }
}
