namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Stores.Resources.ImportBooks;

    public interface IImportBookReportFacadeService
    {
        IResult<ResultsModel> GetImpbookIPRReport(IPRSearchResource resource);

        IResult<IEnumerable<IEnumerable<string>>> GetImpbookIprReportExport(IPRSearchResource resource);
    }
}
