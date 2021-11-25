namespace Linn.Stores.Domain.LinnApps.Tpk
{
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Domain.LinnApps.Tpk.Models;

    public interface ITpkService
    {
        TpkResult TransferStock(TpkRequest tpkRequest);

        ProcessResult UnpickStock(
            int reqNumber,
            int lineNumber,
            int orderNumber,
            int orderLine,
            int amendedBy,
            int? palletNumber,
            int locationId);
    }
}
