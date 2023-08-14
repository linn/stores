namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    public interface IProductUpgradePack
    {
        int GetRenewSernosFromOriginal(int serialNumber);
    }
}
