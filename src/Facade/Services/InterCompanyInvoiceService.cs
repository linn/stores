namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class InterCompanyInvoiceService : IInterCompanyInvoiceService
    {
        private readonly IQueryRepository<InterCompanyInvoice> repository;

        public InterCompanyInvoiceService(IQueryRepository<InterCompanyInvoice> repository)
        {
            this.repository = repository;
        }

        public IResult<IEnumerable<InterCompanyInvoice>> SearchInterCompanyInvoices(string searchTerm)
        {
            return new SuccessResult<IEnumerable<InterCompanyInvoice>>(
                this.repository.FilterBy(i => i.ExportReturnId == int.Parse(searchTerm)));
        }
    }
}