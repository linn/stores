namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class DepartmentsResourceBuilder : IResourceBuilder<IEnumerable<Department>>
    {
        private readonly DepartmentResourceBuilder departmentResourceBuilder = new DepartmentResourceBuilder();

        public IEnumerable<DepartmentResource> Build(IEnumerable<Department> departments)
        {
            return departments
                .Select(a => this.departmentResourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<Department>>.Build(IEnumerable<Department> departments) => this.Build(departments);

        public string GetLocation(IEnumerable<Department> departments)
        {
            throw new NotImplementedException();
        }
    }
}
