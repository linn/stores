namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    public interface IWcsPack
    {
        void MoveDpqPalletsToUpper();

        bool CanMovePalletToUpper(int palletNumber);

        void MovePalletToUpper(int palletNumber, string reference);

        int NextTaskSeq();

        string PalletLocation(int palletNumber);

        int? PalletAtLocation(string location);

        int MovePallet(int palletNumber, string destination, int priority, string taskSource, int who);

        int AtMovePallet(int palletNumber, string fromLocation, string destination, int priority, string taskSource, int who);

        int EmptyLocation(int palletNumber, string location, int priority, string taskSource, int who);
    }
}
