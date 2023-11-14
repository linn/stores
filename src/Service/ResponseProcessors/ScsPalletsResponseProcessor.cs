namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Linn.Common.Facade;
    using Linn.Stores.Domain;

    public class ScsPalletsResponseProcessor : JsonResponseProcessor<IEnumerable<ScsPallet>>
    {
        public ScsPalletsResponseProcessor(IResourceBuilder<IEnumerable<ScsPallet>> resourceBuilder)
            : base(resourceBuilder, "scs-pallets", 1)
        {
        }
    }
}
