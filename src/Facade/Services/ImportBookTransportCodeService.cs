namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ImportBooks;

    public class ImportBookTransportCodeService : IImportBookTransportCodeService
    {
        private readonly IRepository<ImportBookTransportCode, int> transportCodeRepository;

        public ImportBookTransportCodeService(IRepository<ImportBookTransportCode, int> transportCodeRepository)
        {
            this.transportCodeRepository = transportCodeRepository;
        }

        public IResult<IEnumerable<ImportBookTransportCode>> GetTransportCodes()
        {
            var codes = this.transportCodeRepository.FindAll();

            return new SuccessResult<IEnumerable<ImportBookTransportCode>>(codes);
        }
    }
}
