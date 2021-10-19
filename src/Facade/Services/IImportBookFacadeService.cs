namespace Linn.Stores.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Resources.ImportBooks;

    public interface IImportBookFacadeService : IFacadeService<ImportBook, int, ImportBookResource, ImportBookResource>
    {
        IResult<ProcessResult> PostDuty(PostDutyResource resource);
    }
}
