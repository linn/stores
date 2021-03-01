namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    using Linn.Stores.Domain.LinnApps.Models;

    public interface IExportReturnsPack
    {
        MakeExportReturnResult MakeExportReturn(string rsns, string hubReturn);
    }
}