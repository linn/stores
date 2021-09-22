namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    public interface IPalletAnalysisPack
    {
        bool CanPutPartOnPallet(string partNumber, string palletNumber);

        string Message();
    }
}
