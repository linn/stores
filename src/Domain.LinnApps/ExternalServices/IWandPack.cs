namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    using Linn.Stores.Domain.LinnApps.Wand.Models;

    public interface IWandPack
    {
        WandPackResult Wand(string transType, int userNumber, int consignmentId, string wandString);
    }
}
