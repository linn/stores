namespace Linn.Stores.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Reporting.Models;
    using Linn.Stores.Resources.ImportBooks;

    public interface IImportBookReportFacadeService
    {
        IResult<ResultsModel> GetImpbookIPRReport(IPRSearchResource resource);
    }
}
