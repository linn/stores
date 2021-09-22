namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class EmployeesResourceBuilder : IResourceBuilder<IEnumerable<Employee>>
    {
        private readonly EmployeeResourceBuilder employeeResourceBuilder = new EmployeeResourceBuilder();

        public IEnumerable<EmployeeResource> Build(IEnumerable<Employee> employees)
        {
            return employees
                .Select(a => this.employeeResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<Employee>>.Build(IEnumerable<Employee> employees) => this.Build(employees);

        public string GetLocation(IEnumerable<Employee> employees)
        {
            throw new NotImplementedException();
        }
    }
}
