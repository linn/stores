namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.InterCompanyInvoices;

    public class InterCompanyInvoiceService : IInterCompanyInvoiceService
    {
        private readonly IQueryRepository<InterCompanyInvoice> repository;

        public InterCompanyInvoiceService(IQueryRepository<InterCompanyInvoice> repository)
        {
            this.repository = repository;
        }

        public IResult<InterCompanyInvoice> GetByDocumentNumber(int id)
        {
            var invoice = this.repository.FindBy(i => i.DocumentNumber == id);
            if (invoice == null)
            {
                return new NotFoundResult<InterCompanyInvoice>("Document Not Found");
            }

            return new SuccessResult<InterCompanyInvoice>(invoice);
        }

        public IResult<IEnumerable<InterCompanyInvoice>> SearchInterCompanyInvoices(string searchTerm)
        {
            return new SuccessResult<IEnumerable<InterCompanyInvoice>>(
                this.repository.FilterBy(i => i.ExportReturnId == int.Parse(searchTerm)));
        }
    }
}
