namespace Linn.Stores.Facade.Services
{
    using System.Threading.Tasks;
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Resources.RequestResources;

    public interface ILogisticsProcessesFacadeService
    {
        IResult<ProcessResult> PrintLabel(LogisticsLabelRequestResource resource);

        Task<IResult<ProcessResult>> PrintConsignmentDocuments(ConsignmentRequestResource resource);

        IResult<ProcessResult> SaveConsignmentDocuments(ConsignmentRequestResource resource);
    }
}
