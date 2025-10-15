namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps;

    public class PrintResultResponseProcessor : JsonResponseProcessor<PrintResult>
    {
        public PrintResultResponseProcessor(IResourceBuilder<PrintResult> resourceBuilder)
            : base(resourceBuilder, "print-result", 1)
        {
        }
    }
}
