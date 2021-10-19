namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    public interface IPurchaseLedgerPack
    {
        int GetLedgerPeriod();

        int GetNextLedgerSeq();

        int GetNomacc(string department, string nom);
    }
}
