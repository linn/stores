namespace Linn.Stores.Domain.LinnApps.Wand
{
    using Linn.Stores.Domain.LinnApps.Wand.Models;

    public interface IWandService
    {
        WandResult Wand(string wandAction, string wandString, int consignmentId);
    }
}
