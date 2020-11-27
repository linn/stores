namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;

    public interface IProductAnalysisCodeService
    {
        IResult<IEnumerable<ProductAnalysisCode>> GetProductAnalysisCodes(string searchTerm = null);
    }
}
