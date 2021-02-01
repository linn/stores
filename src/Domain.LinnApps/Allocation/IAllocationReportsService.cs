namespace Linn.Stores.Domain.LinnApps.Allocation
{
    using Linn.Common.Reporting.Models;

    public interface IAllocationReportsService
    {
        ResultsModel DespatchPickingSummary();
    }
}
