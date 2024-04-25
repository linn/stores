namespace Linn.Stores.Service.ResultHandlers
{
    using Linn.Common.Service.Core.Handlers;
    using Linn.Stores.Resources.Requisitions;

    public class RequisitionMovesResultHandler : JsonResultHandler<RequisitionResource>
    {
        public RequisitionMovesResultHandler() : base("application/vnd.linn.req-moves-summary+json;version=1") 
        {
        }
    }
}
