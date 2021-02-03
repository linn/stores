namespace Linn.Stores.Domain.LinnApps.Allocation
{
    using Linn.Common.Reporting.Models;
    using Linn.Stores.Domain.LinnApps.Allocation.Models;

    public interface IAllocationReportsService
    {
        ResultsModel DespatchPickingSummary();

        DespatchPalletQueueResult DespatchPalletQueue();
    }
}
