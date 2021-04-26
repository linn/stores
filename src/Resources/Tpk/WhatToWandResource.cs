namespace Linn.Stores.Resources.Tpk
{
    using System.Collections.Generic;

    public class WhatToWandResource
    {
        public IEnumerable<WhatToWandLineResource> Lines { get; set; }

        public ConsignmentResource Consignment { get; set; }

        public string Type { get; set; }

        public SalesAccountResource Account { get; set; }
    }
}
