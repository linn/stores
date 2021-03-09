namespace Linn.Stores.Domain.LinnApps.Wand
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Wand.Models;

    public class WandService : IWandService
    {
        private readonly IWandPack wandPack;

        private readonly IRepository<WandLog, int> wandLogRepository;

        public WandService(IWandPack wandPack, IRepository<WandLog, int> wandLogRepository)
        {
            this.wandPack = wandPack;
            this.wandLogRepository = wandLogRepository;
        }

        public WandResult Wand(string wandAction, string wandString, int consignmentId, int userNumber)
        {
            var wandPackResult = this.wandPack.Wand(wandAction, userNumber, consignmentId, wandString);
            var result = new WandResult
                             {
                                 ConsignmentId = consignmentId,
                                 WandString = wandString,
                                 Success = wandPackResult.Success,
                                 Message = wandPackResult.Message
                             };

            if (wandPackResult.WandLogId.HasValue)
            {
                result.WandLog = this.wandLogRepository.FindById(wandPackResult.WandLogId.Value);
            }

            return result;
        }
    }
}
