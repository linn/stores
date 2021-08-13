namespace Linn.Stores.Domain.LinnApps.Consignments
{
    using System;
    using System.Linq;

    using Linn.Common.Domain.LinnApps.RemoteServices;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Models;

    public class LogisticsLabelService : ILogisticsLabelService
    {
        private readonly IBartenderLabelPack bartenderLabelPack;

        private readonly IRepository<Consignment, int> consignmentRepository;

        private readonly IRepository<PrinterMapping, int> printerMappingRepository;

        public LogisticsLabelService(
            IBartenderLabelPack bartenderLabelPack,
            IRepository<Consignment, int> consignmentRepository,
            IRepository<PrinterMapping, int> printerMappingRepository)
        {
            this.bartenderLabelPack = bartenderLabelPack;
            this.consignmentRepository = consignmentRepository;
            this.printerMappingRepository = printerMappingRepository;
        }

        public ProcessResult PrintCartonLabel(int consignmentId, int firstCarton, int? lastCarton, int userNumber)
        {
            var consignment = this.consignmentRepository.FindById(consignmentId);

            if (!lastCarton.HasValue)
            {
                lastCarton = firstCarton;
            }

            var labelMessage = string.Empty;
            for (int i = firstCarton; i <= lastCarton.Value; i++)
            {
                var labelData = $"\"{this.GetPrintAddress(consignment.Address)}\", \"{this.GetLabelInformation(consignment, i)}\"";
                var printerName = this.GetPrinter(userNumber);

                this.bartenderLabelPack.PrintLabels(
                    $"CartonLabel{consignmentId}item{i}",
                    printerName,
                    1,
                    "dispatchaddress.btw",
                    labelData,
                    ref labelMessage);
            }

            return new ProcessResult(true, "ok");
        }

        private string GetLabelInformation(Consignment consignment, int cartonNumber)
        {
            var item = consignment.Items.First(a => a.ContainerNumber == cartonNumber);

            return
                $"Carton: {item.ContainerNumber}{Environment.NewLine}Article:{item.ItemDescription}{Environment.NewLine}Serial No: {item.SerialNumber}{Environment.NewLine}Order: {item.OrderNumber}{Environment.NewLine}Consignment: {consignment.ConsignmentId}";
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
