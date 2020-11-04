namespace Linn.Stores.Domain.LinnApps
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using Linn.Stores.Domain.LinnApps.Parts;

    public class RootProduct
    {   
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? DateInvalid { get; set; }
    }
}
