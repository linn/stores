namespace Linn.Stores.Domain.LinnApps.Consignments
{
    using System;
    using System.Linq;

    using Linn.Common.Domain.LinnApps.RemoteServices;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.Models;

    public class LogisticsLabelService : ILogisticsLabelService
    {
        private readonly IBartenderLabelPack bartenderLabelPack;

        private readonly IRepository<Consignment, int> consignmentRepository;

        private readonly IRepository<PrinterMapping, int> printerMappingRepository;

        private readonly IRepository<Address, int> addressRepository;

        public LogisticsLabelService(
            IBartenderLabelPack bartenderLabelPack,
            IRepository<Consignment, int> consignmentRepository,
            IRepository<PrinterMapping, int> printerMappingRepository,
            IRepository<Address, int> addressRepository)
        {
            this.bartenderLabelPack = bartenderLabelPack;
            this.consignmentRepository = consignmentRepository;
            this.printerMappingRepository = printerMappingRepository;
            this.addressRepository = addressRepository;
        }

        public ProcessResult PrintCartonLabel(
            int consignmentId,
            int firstCarton,
            int? lastCarton,
            int userNumber,
            int numberOfCopies = 1)
        {
            var consignment = this.consignmentRepository.FindById(consignmentId);
            var labelCount = 0;

            if (!lastCarton.HasValue || lastCarton < firstCarton)
            {
                lastCarton = firstCarton;
            }

            var labelMessage = string.Empty;
            for (int i = firstCarton; i <= lastCarton.Value; i++)
            {
                string labelData = null;
                try
                {
                    labelData = $"\"{this.GetPrintAddress(consignment.Address)}\", \"{this.GetLabelInformation(consignment, i)}\"";
                }
                catch (ProcessException exception)
                {
                    return new ProcessResult(false, exception.Message);
                }

                var printerName = this.GetPrinter(userNumber);

                this.bartenderLabelPack.PrintLabels(
                    $"CartonLabel{consignmentId}item{i}",
                    printerName,
                    numberOfCopies,
                    "dispatchaddress.btw",
                    labelData,
                    ref labelMessage);

                labelCount += numberOfCopies;
            }

            return new ProcessResult(true, $"{labelCount} carton label(s) printed");
        }

        public ProcessResult PrintPalletLabel(
            int consignmentId,
            int firstPallet,
            int? lastPallet,
            int userNumber,
            int numberOfCopies = 1)
        {
            var consignment = this.consignmentRepository.FindById(consignmentId);
            var labelCount = 0;

            if (!lastPallet.HasValue || lastPallet < firstPallet)
            {
                lastPallet = firstPallet;
            }

            var labelMessage = string.Empty;
            for (int i = firstPallet; i <= lastPallet.Value; i++)
            {
                string labelData = null;
                try
                {
                    labelData = this.GetPalletLabelInfo(consignment, i);
                }
                catch (ProcessException exception)
                {
                    return new ProcessResult(false, exception.Message);
                }

                var printerName = this.GetPrinter(userNumber);

                this.bartenderLabelPack.PrintLabels(
                    $"PalletLabel{consignmentId}pallet{i}",
                    printerName,
                    numberOfCopies,
                    "dispatchpallet.btw",
                    labelData,
                    ref labelMessage);

                labelCount += numberOfCopies;
            }

            return new ProcessResult(true, $"{labelCount} pallet label(s) printed");
        }

        public ProcessResult PrintAddressLabel(int addressId, int userNumber, int numberOfCopies = 1)
        {
            var address = this.addressRepository.FindById(addressId);
            if (address == null)
            {
                return new ProcessResult(false, $"no address found for {addressId}");
            }

            string labelData = null;
            try
            {
                labelData = $"\"{this.GetPrintAddress(address)}\", \"\"";
            }
            catch (ProcessException exception)
            {
                return new ProcessResult(false, exception.Message);
            }

            var printerName = this.GetPrinter(userNumber);
            var labelMessage = string.Empty;

            this.bartenderLabelPack.PrintLabels(
                $"AddressLabel{addressId}",
                printerName,
                numberOfCopies,
                "dispatchaddress.btw",
                labelData,
                ref labelMessage);

            return new ProcessResult(true, $"{numberOfCopies} address label(s) printed");
        }

        private string GetPalletLabelInfo(Consignment consignment, int palletNumber)
        {
            var pallet = consignment.Pallets.FirstOrDefault(a => a.PalletNumber == palletNumber);

            if (pallet == null)
            {
                throw new ProcessException(
                    $"Printing Failed. Could not find pallet {palletNumber} on consignment {consignment.ConsignmentId}");
            }

            var labelInfo = $"\"{this.GetPrintAddress(consignment.Address)}\"";
            labelInfo += $", \"{palletNumber}\"";
            labelInfo += $", \"Pallet Number {palletNumber}\"";
            labelInfo += $", \"{pallet.Weight}\"";
            labelInfo += $", \"{consignment.ConsignmentId}\"";

            return labelInfo;
        }

        private string GetLabelInformation(Consignment consignment, int cartonNumber)
        {
            if (consignment == null)
            {
                return
                    $"Carton: {Environment.NewLine}Article:{Environment.NewLine}Serial No: {Environment.NewLine}Order: {Environment.NewLine}Consignment: ";
            }

            var item = consignment.Items.FirstOrDefault(a =>
                a.ContainerNumber == cartonNumber && (a.ItemType == "C" || a.ItemType == "S"));

            if (item == null)
            {
                throw new ProcessException(
                    $"Printing Failed. Could not find carton {cartonNumber} on consignment {consignment.ConsignmentId}");
            }

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
