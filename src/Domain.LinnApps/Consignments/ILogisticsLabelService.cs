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
            int userNumber,
            int numberOfCopies = 1);
    }
}
