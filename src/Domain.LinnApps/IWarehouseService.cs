namespace Linn.Stores.Domain.LinnApps
{
    public interface IWarehouseService
    {
        string MoveAllPalletsToUpper();

        string MovePalletToUpper(int palletNumber, string reference);
    }
}
