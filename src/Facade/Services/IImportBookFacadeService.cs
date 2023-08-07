namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Resources.ImportBooks;

    public interface IImportBookFacadeService : IFacadeFilterService<ImportBook, int, ImportBookResource, ImportBookResource, ImportBookSearchResource>
    {
        IResult<ProcessResult> PostDuty(PostDutyResource resource, IEnumerable<string> privileges);
    }
}
