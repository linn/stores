namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Workstation.Models;

    public class WorkstationTopUpStatusResponseProcessor : JsonResponseProcessor<ResponseModel<WorkstationTopUpStatus>>
    {
        public WorkstationTopUpStatusResponseProcessor(IResourceBuilder<ResponseModel<WorkstationTopUpStatus>> resourceBuilder)
            : base(resourceBuilder, "workstation-top-up-status", 1)
        {
        }
    }
}
