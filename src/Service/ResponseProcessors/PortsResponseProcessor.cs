namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;

    public class PortsResponseProcessor : JsonResponseProcessor<IEnumerable<Port>>
    {
        public PortsResponseProcessor(
            IResourceBuilder<IEnumerable<Port>> resourceBuilder)
            : base(resourceBuilder, "linnapps-import-books-ports", 1)
        {
        }
    }
}
