namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps;

    public class EmployeesResponseProcessor : JsonResponseProcessor<IEnumerable<Employee>>
    {
        public EmployeesResponseProcessor(IResourceBuilder<IEnumerable<Employee>> resourceBuilder)
            : base(resourceBuilder, "employees", 1)
        {
        }
    }
}
