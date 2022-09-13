namespace Linn.Stores.Domain.LinnApps.Wand
{
    using System;

    using Linn.Common.Domain.LinnApps.RemoteServices;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Wand.Models;

    public class WandService : IWandService
    {
        private readonly IWandPack wandPack;

        private readonly IRepository<WandLog, int> wandLogRepository;

        private readonly IRepository<Consignment, int> consignmentRepository;

        private readonly IBartenderLabelPack bartenderLabelPack;

        private readonly IRepository<PrinterMapping, int> printerMappingRepository;

        public WandService(
            IWandPack wandPack,
            IRepository<WandLog, int> wandLogRepository,
            IRepository<Consignment, int> consignmentRepository,
            IBartenderLabelPack bartenderLabelPack,
            IRepository<PrinterMapping, int> printerMappingRepository)
        {
            this.wandPack = wandPack;
            this.wandLogRepository = wandLogRepository;
            this.consignmentRepository = consignmentRepository;
            this.bartenderLabelPack = bartenderLabelPack;
            this.printerMappingRepository = printerMappingRepository;
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

        public WandResult Wand(
            string wandAction,
            string wandString,
            int consignmentId,
            int userNumber,
            bool printLabels)
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
                this.MaybePrintLabel(printLabels, consignmentId, result.WandLog, userNumber);
            }

            return result;
        }

        private void MaybePrintLabel(bool printLabels, int consignmentId, WandLog wandLog, int userNumber)
        {
            if (!printLabels || !wandLog.ContainerNo.HasValue || wandLog.TransType != "W")
            {
                return;
            }

            var consignment = this.consignmentRepository.FindById(consignmentId);

            var labelMessage = string.Empty;
            var labelData = $"\"{this.GetPrintAddress(consignment.Address)}\", \"{this.GetLabelInformation(wandLog)}\"";
            var printerName = this.GetPrinter(userNumber);
            if (consignment.Address.CountryCode != "GB")
            {
                this.bartenderLabelPack.PrintLabels(
                    $"Address{wandLog.Id}",
                    printerName,
                    1,
                    "dispatchaddress.btw",
                    labelData,
                    ref labelMessage);
            }
        }

        private string GetPrinter(int userNumber)
        {
            var printer = this.printerMappingRepository.FindBy(
                a => a.UserNumber == userNumber && a.PrinterGroup == "DISPATCH-LABEL");

            if (!string.IsNullOrEmpty(printer?.PrinterName))
            {
                return printer.PrinterName;
            }

            printer = this.printerMappingRepository.FindBy(
                a => a.DefaultForGroup == "Y" && a.PrinterGroup == "DISPATCH-LABEL");

            return printer?.PrinterName;
        }

        private string GetLabelInformation(WandLog wandLog)
        {
            return
                $"Carton: {wandLog.ContainerNo}{Environment.NewLine}Article:{wandLog.ArticleNumber}{Environment.NewLine}Serial No: {wandLog.SeriaNumber1} {wandLog.SeriaNumber2}{Environment.NewLine}Order: {wandLog.OrderNumber}{Environment.NewLine}Consignments: {wandLog.ConsignmentId}";
        }

        private string GetPrintAddress(Address address)
        {
            var printAddress = $"{address.Addressee}{Environment.NewLine}";
            printAddress += string.IsNullOrEmpty(address.Addressee2) ? null : $"{address.Addressee2}{Environment.NewLine}";
            printAddress += string.IsNullOrEmpty(address.Line1) ? null : $"{address.Line1}{Environment.NewLine}";
            printAddress += string.IsNullOrEmpty(address.Line2) ? null : $"{address.Line2}{Environment.NewLine}";
            printAddress += string.IsNullOrEmpty(address.Line3) ? null : $"{address.Line3}{Environment.NewLine}";
            printAddress += string.IsNullOrEmpty(address.Line4) ? null : $"{address.Line4}{Environment.NewLine}";
            printAddress += string.IsNullOrEmpty(address.PostCode) ? null : $"{address.PostCode}{Environment.NewLine}";
            printAddress += string.IsNullOrEmpty(address.Country.DisplayName) ? null : $"{address.Country.DisplayName}";

            return printAddress;
        }
    }
}
