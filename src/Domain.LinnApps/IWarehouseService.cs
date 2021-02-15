namespace Linn.Stores.Domain.LinnApps
{
    using Linn.Stores.Domain.LinnApps.Models;

    public interface IWarehouseService
    {
        MessageResult MoveAllPalletsToUpper();

        MessageResult MovePalletToUpper(int palletNumber, string reference);
    }
}
