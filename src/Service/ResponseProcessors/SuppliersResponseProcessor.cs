namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;


    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps;

    public class SuppliersResponseProcessor : JsonResponseProcessor<IEnumerable<Supplier>>
    {
        public SuppliersResponseProcessor(IResourceBuilder<IEnumerable<Supplier>> resourceBuilder)
            : base(resourceBuilder, "suppliers", 1)
        {
        }
    }
}