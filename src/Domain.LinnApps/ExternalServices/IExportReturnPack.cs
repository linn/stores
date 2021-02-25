namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    public interface IExportReturnPack
    {
        int MakeExportReturn(string rsns, bool hubReturn);
    }
}
