namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps;

    public class ExportRsnsResponseProcessor : JsonResponseProcessor<IEnumerable<ExportRsn>>
    {
        public ExportRsnsResponseProcessor(IResourceBuilder<IEnumerable<ExportRsn>> resourceBuilder)
            : base(resourceBuilder, "export-rsns", 1)
        {
        }
    }
}