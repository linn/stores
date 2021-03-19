namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Domain.LinnApps.StockMove.Models;

    public interface IStoresPack
    {
        ProcessResult UnAllocateRequisition(int reqNumber, int? reqLineNumber, int userNumber);

        RequisitionProcessResult CreateMoveReq(int userNumber);
    }
}
