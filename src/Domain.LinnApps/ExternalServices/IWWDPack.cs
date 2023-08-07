namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    public interface IWwdPack
    {
        int WWD(string partNumber, string workStationCode, int quantity);
    }
}
