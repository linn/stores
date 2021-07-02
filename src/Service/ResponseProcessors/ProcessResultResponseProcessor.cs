namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Models;

    public class ProcessResultResponseProcessor : JsonResponseProcessor<ProcessResult>
    {
        public ProcessResultResponseProcessor(IResourceBuilder<ProcessResult> resourceBuilder)
            : base(resourceBuilder, "process-result", 1)
        {
        }
    }
}
