namespace Linn.Stores.Domain.LinnApps.Tpk
{
    using Linn.Stores.Domain.LinnApps.Tpk.Models;

    public interface ITpkService
    {
        TpkResult TransferStock(TpkRequest tpkRequest);
    }
}
