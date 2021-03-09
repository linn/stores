namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;

    public interface ISalesAccountService
    {
        IResult<IEnumerable<SalesAccount>> SearchSalesAccounts(string searchTerm);
    }
}