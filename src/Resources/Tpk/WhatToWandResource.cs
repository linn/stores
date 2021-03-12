namespace Linn.Stores.Resources.Tpk
{
    using System.Collections.Generic;

    public class WhatToWandResource
    {
        public IEnumerable<WhatToWandLineResource> Lines { get; set; }

        public ConsignmentResource Consignment { get; set; }

        public string Type { get; set; }

        public SalesOutletResource Outlet { get; set; }
    }
}
