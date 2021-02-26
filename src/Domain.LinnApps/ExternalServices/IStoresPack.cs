namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    using Linn.Stores.Domain.LinnApps.Models;

    public interface IStoresPack
    {
        ProcessResult UnAllocateRequisition(int reqNumber, int? reqLineNumber, int userNumber);
    }
}
