namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ImportBooks;

    public class ImportBookTransactionCodeService : IImportBookTransactionCodeService
    {
        private readonly IRepository<ImportBookTransactionCode, int> transactionCodeRepository;

        public ImportBookTransactionCodeService(IRepository<ImportBookTransactionCode, int> transactionCodeRepository)
        {
            this.transactionCodeRepository = transactionCodeRepository;
        }

        public IResult<IEnumerable<ImportBookTransactionCode>> GetTransactionCodes()
        {
            var codes = this.transactionCodeRepository.FindAll();

            return new SuccessResult<IEnumerable<ImportBookTransactionCode>>(codes);
        }
    }
}
