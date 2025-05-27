namespace Linn.Stores.Domain.LinnApps.Consignments
{
    using Linn.Stores.Domain.LinnApps.Models;

    public interface ILogisticsLabelService
    {
        ProcessResult PrintCartonLabel(
            int consignmentId,
            int firstCarton,
            int? lastCarton,
            int userNumber,
            int numberOfCopies = 1);

        ProcessResult PrintPalletLabel(
            int consignmentId,
            int firstPallet,
            int? lastPallet,
            int userNumber,
            int numberOfCopies = 1);

        ProcessResult PrintAddressLabel(
            int addressId,
            string line1,
            string line2,
            int userNumber,
            int numberOfCopies = 1);

        ProcessResult PrintGeneralLabel(
            string line1,
            string line2,
            string line3,
            string line4,
            string line5,
            int userNumber,
            int numberOfCopies = 1);
    }
}
