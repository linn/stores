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
    }
}
