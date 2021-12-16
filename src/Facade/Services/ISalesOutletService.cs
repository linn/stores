namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;

    public interface ISalesOutletService
    {
        IResult<IEnumerable<SalesOutlet>> SearchSalesOutlets(string searchTerm);

        IResult<IEnumerable<SalesOutlet>> GetByOrders(IEnumerable<int> orderNumbers);

        IResult<IEnumerable<SalesOutlet>> GetOutletAddresses(int? accountId, string searchTerm);
    }
}