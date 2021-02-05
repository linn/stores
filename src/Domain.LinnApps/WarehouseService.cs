namespace Linn.Stores.Domain.LinnApps
{
    using Linn.Stores.Domain.LinnApps.ExternalServices;

    public class WarehouseService : IWarehouseService
    {
        private readonly IWcsPack wcsPack;

        public WarehouseService(IWcsPack wcsPack)
        {
            this.wcsPack = wcsPack;
        }

        public string MoveAllPalletsToUpper()
        {
            this.wcsPack.MoveDpqPalletsToUpper();
            return "Move pallets to upper called successfully";
        }

        public string MovePalletToUpper(int palletNumber, string reference)
        {
            if (!this.wcsPack.CanMovePalletToUpper(palletNumber))
            {
                return $"Pallet {palletNumber} can no longer be moved to upper";
            }

            this.wcsPack.MovePalletToUpper(palletNumber, reference);
            return $"Pallet {palletNumber} move to upper called successfully";
        }
    }
}
