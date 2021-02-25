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

        public WandResult Wand(string wandAction, string wandString, int consignmentId, int userNumber)
        {
            var result = this.wandPack.Wand(wandAction, userNumber, consignmentId, wandString);
            result.ConsignmentId = consignmentId;
            result.WandString = wandString;

            return result;
        }
    }
}
