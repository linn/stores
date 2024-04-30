namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Resources;

    public interface IAccountingCompanyFacadeService
    {
        IResult<IEnumerable<AccountingCompanyResource>> GetValid();
    }
}
