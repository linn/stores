namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    public interface ITpkOoPack
    {
        string GetTpkNotes(int consignmentId, string fromLocation);

        void UpdateQuantityPrinted(string fromLocation, out bool success);
    }
}
