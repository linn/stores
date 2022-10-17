namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Tpk.Models;

    public class WhatToWandConsignmentResponseProcessor : JsonResponseProcessor<WhatToWandConsignment>
    {
        public WhatToWandConsignmentResponseProcessor(IResourceBuilder<WhatToWandConsignment> resourceBuilder)
            : base(resourceBuilder, "what-to-wand", 1)
        {
        }
    }
}
