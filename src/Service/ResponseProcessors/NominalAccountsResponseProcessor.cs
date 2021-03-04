namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps;

    public class NominalAccountsResponseProcessor : JsonResponseProcessor<IEnumerable<NominalAccount>>
    {
        public NominalAccountsResponseProcessor(IResourceBuilder<IEnumerable<NominalAccount>> resourceBuilder)
            : base(resourceBuilder, "nominal-accounts", 1)
        {
        }
    }
}
