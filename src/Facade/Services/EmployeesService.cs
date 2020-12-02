namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class EmployeesService : IEmployeeService
    {
        private readonly IRepository<Employee, int> repository;

        public EmployeesService(IRepository<Employee, int> repository)
        {
            this.repository = repository;
        }

        public IResult<IEnumerable<Employee>> SearchEmployees(string name, bool returnInvalid = false)
        {
            var result = this.repository.FilterBy(e => e.FullName.ToUpper().Contains(name.ToUpper()));
            
            if (!returnInvalid)
            {
                result = result.Where(e => e.DateInvalid == null);
            }

            return new SuccessResult<IEnumerable<Employee>>(result);
        }
    }
}
