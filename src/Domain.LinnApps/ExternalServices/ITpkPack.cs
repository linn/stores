namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    public interface ITpkPack
    {
        string GetTpkNotes(int consignmentId, string fromLocation);

        string GetWhatToWandType(int consignmentId, string storagePlace);

        void UpdateQuantityPrinted(string fromLocation, out bool success);
    }
}
