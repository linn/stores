namespace Linn.Stores.Domain.LinnApps.ImportBooks
{
    using System.Collections.Generic;

    public interface IImportBookService
    {
        void Update(ImportBook from, ImportBook to);

        IEnumerable<ImportBookExchangeRate> GetExchangeRates(string date);
    }
}
