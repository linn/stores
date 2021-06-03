namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;

    public interface IImportBookExchangeRateService
    {
        IResult<IEnumerable<ImportBookExchangeRate>> GetExchangeRatesForDate(string date);
    }
}
