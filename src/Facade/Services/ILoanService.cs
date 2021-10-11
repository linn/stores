namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;

    public interface ILoanService
    {
        IResult<IEnumerable<Loan>> Search(string searchTerm);
    }
}
