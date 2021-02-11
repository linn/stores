namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    public interface IWcsPack
    {
        void MoveDpqPalletsToUpper();

        bool CanMovePalletToUpper(int palletNumber);

        void MovePalletToUpper(int palletNumber, string reference);
    }
}
