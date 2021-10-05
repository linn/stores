namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;

    public interface ISuppliersService
    {
        IResult<IEnumerable<Supplier>> GetSuppliers(string searchTerm = null, bool returnClosed = false, bool returnOnlyApprovedCarriers = false);
    }
}
