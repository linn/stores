namespace Linn.Stores.Domain.LinnApps.Wand
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Wand.Models;

    public class WandService : IWandService
    {
        private readonly IWandPack wandPack;

        private readonly IRepository<WandLog, int> wandLogRepository;

        private readonly IQueryRepository<Consignment> consignmentRepository;

        private readonly IBundleLabelPack bundleLabelPack;

        public WandService(
            IWandPack wandPack,
            IRepository<WandLog, int> wandLogRepository,
            IQueryRepository<Consignment> consignmentRepository,
            IBundleLabelPack bundleLabelPack)
        {
            this.wandPack = wandPack;
            this.wandLogRepository = wandLogRepository;
            this.consignmentRepository = consignmentRepository;
            this.bundleLabelPack = bundleLabelPack;
        }

        public static string WandStringSuggestion(
            string typeOfSerialNumber,
            int boxesPerProduct,
            int quantity,
            string linnBarCode)
        {
            if (typeOfSerialNumber == "N")
            {
                if (boxesPerProduct > 1)
                {
                    return $"22{linnBarCode}{boxesPerProduct}?";
                }

                return $"12{linnBarCode}/{quantity}";
            }

            return $"02{linnBarCode}?";
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

            this.MaybePrintLabel(consignmentId, result.WandLog);

            return result;
        }

        private void MaybePrintLabel(int consignmentId, WandLog wandLog)
        {
            if (!wandLog.ContainerNo.HasValue || wandLog.TransType != "W")
            {
                return;
            }

            var consignment = this.consignmentRepository.FindBy(c => c.ConsignmentId == consignmentId);

            if (consignment.CountryCode != "GB")
            {
                // print
            }
        }
    }
}
