namespace Linn.Stores.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Resources.RequestResources;

    public interface ILogisticsProcessesFacadeService
    {
        IResult<ProcessResult> PrintLabel(LogisticsLabelRequestResource resource);

        IResult<ProcessResult> PrintConsignmentDocuments(ConsignmentRequestResource resource);
    }
}
