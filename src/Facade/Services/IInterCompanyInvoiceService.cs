namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.InterCompanyInvoices;

    public interface IInterCompanyInvoiceService
    {
        IResult<IEnumerable<InterCompanyInvoice>> SearchInterCompanyInvoices(string searchTerm);
    }
}