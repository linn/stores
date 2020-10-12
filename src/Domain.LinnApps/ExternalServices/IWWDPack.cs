namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    public interface IWwdPack
    {
        void WWD(string partNumber, string workStationCode, int quantity);

        int JobId();
    }
}
