namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    public interface ITpkPack
    {
        string GetTpkNotes(int consignmentId, string fromLocation);

        string GetWhatToWandType(int consignmentId);

        void UpdateQuantityPrinted(string fromLocation, out bool success);
    }
}
