namespace Linn.Stores.Domain.LinnApps.Wand
{
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Wand.Models;

    public class WandService : IWandService
    {
        private readonly IWandPack wandPack;

        public WandService(IWandPack wandPack)
        {
            this.wandPack = wandPack;
        }

        public WandResult Wand(string wandAction, string wandString, int consignmentId)
        {
            return this.wandPack.Wand(wandAction, 100, consignmentId, wandString);
        }
    }
}
