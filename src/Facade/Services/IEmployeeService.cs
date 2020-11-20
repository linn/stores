namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;

    public interface IEmployeeService
    {
        IResult<IEnumerable<Employee>> SearchEmployees(string name, bool returnInvalid = false);
    }
}
