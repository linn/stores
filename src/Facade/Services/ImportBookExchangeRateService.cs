namespace Linn.Stores.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ImportBooks;

    using System.Collections.Generic;

    public class ImportBookExchangeRateService : IImportBookExchangeRateService
    {
        private IImportBookService importBookService;

        public ImportBookExchangeRateService(
            IRepository<ImportBookExchangeRate, ImportBookExchangeRateKey> repository,
            IImportBookService importBookService)
        {
            this.importBookService = importBookService;
        }

        public IResult<IEnumerable<ImportBookExchangeRate>> GetExchangeRatesForDate(string date)
        {
            if (string.IsNullOrWhiteSpace(date))
            {
                return new BadRequestResult<IEnumerable<ImportBookExchangeRate>>("Date is required");
            }

            var result = this.importBookService.GetExchangeRates(date);
            return new SuccessResult<IEnumerable<ImportBookExchangeRate>>(result);
        }
    }
}
