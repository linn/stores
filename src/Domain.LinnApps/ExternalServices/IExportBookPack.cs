namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    using Linn.Stores.Domain.LinnApps.Models;

    public interface IExportBookPack
    {
        ProcessResult MakeExportBookFromConsignment(int consignmentId);
    }
}
