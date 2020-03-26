namespace Linn.Stores.Facade.Services
{
    using System.Collections;
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;

    public interface IDepartmentsService
    {
        IResult<IEnumerable<Department>> GetOpenDepartments(string searchTerm = null);
    }
}