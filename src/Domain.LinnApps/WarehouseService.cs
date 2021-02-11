namespace Linn.Stores.Domain.LinnApps
{
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Models;

    public class WarehouseService : IWarehouseService
    {
        private readonly IWcsPack wcsPack;

        public WarehouseService(IWcsPack wcsPack)
        {
            this.wcsPack = wcsPack;
        }

        public MessageResult MoveAllPalletsToUpper()
        {
            this.wcsPack.MoveDpqPalletsToUpper();
            return new MessageResult("Move pallets to upper called successfully");
        }

        public MessageResult MovePalletToUpper(int palletNumber, string reference)
        {
            if (!this.wcsPack.CanMovePalletToUpper(palletNumber))
            {
                return new MessageResult($"Pallet {palletNumber} can no longer be moved to upper");
            }

            this.wcsPack.MovePalletToUpper(palletNumber, reference);
            return new MessageResult($"Pallet {palletNumber} move to upper called successfully");
        }
    }
}
