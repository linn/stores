namespace Linn.Stores.Domain.LinnApps.Wand
{
    using Linn.Common.Domain.LinnApps.RemoteServices;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Wand.Models;

    public class WandService : IWandService
    {
        private readonly IWandPack wandPack;

        private readonly IRepository<WandLog, int> wandLogRepository;

        private readonly IQueryRepository<Consignment> consignmentRepository;

        private readonly IBartenderLabelPack bartenderLabelPack;

        public WandService(
            IWandPack wandPack,
            IRepository<WandLog, int> wandLogRepository,
            IQueryRepository<Consignment> consignmentRepository,
            IBartenderLabelPack bartenderLabelPack)
        {
            this.wandPack = wandPack;
            this.wandLogRepository = wandLogRepository;
            this.consignmentRepository = consignmentRepository;
            this.bartenderLabelPack = bartenderLabelPack;
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
                this.MaybePrintLabel(consignmentId, result.WandLog);
            }

            return result;
        }

        private void MaybePrintLabel(int consignmentId, WandLog wandLog)
        {
            if (!wandLog.ContainerNo.HasValue || wandLog.TransType != "W")
            {
                return;
            }

            var consignment = this.consignmentRepository.FindBy(c => c.ConsignmentId == consignmentId);

            var labelMessage = string.Empty;
            var labelData = $"\"{this.GetPrintAddress(consignment.Address)}\", \"{this.GetLabelInformation(wandLog)}\"";
            if (consignment.Address.CountryCode != "GB")
            {
                this.bartenderLabelPack.PrintLabels(
                    $"Address{wandLog.Id}",
                    "DispatchLabels1",
                    1,
                    "dispatchaddress.btw",
                    labelData,
                    ref labelMessage);
            }
        }

        private string GetLabelInformation(WandLog wandLog)
        {
            return
                $"Carton: {wandLog.ContainerNo}/nArticle:{wandLog.ArticleNumber}/nSerial No: {wandLog.SeriaNumber1} {wandLog.SeriaNumber2}/nOrder: {wandLog.OrderNumber}/nConsignment: {wandLog.ConsignmentId}";
        }

        private string GetPrintAddress(Address address)
        {
            var printAddress = $"{address.Addressee}/n";
            printAddress += string.IsNullOrEmpty(address.Addressee2) ? null : $"{address.Addressee2}/n";
            printAddress += string.IsNullOrEmpty(address.Line1) ? null : $"{address.Line1}/n";
            printAddress += string.IsNullOrEmpty(address.Line2) ? null : $"{address.Line2}/n";
            printAddress += string.IsNullOrEmpty(address.Line3) ? null : $"{address.Line3}/n";
            printAddress += string.IsNullOrEmpty(address.Line4) ? null : $"{address.Line4}/n";
            printAddress += string.IsNullOrEmpty(address.PostCode) ? null : $"{address.PostCode}/n";
            printAddress += string.IsNullOrEmpty(address.Country.DisplayName) ? null : $"{address.Country.DisplayName}";

            return printAddress;
        }
    }
}
