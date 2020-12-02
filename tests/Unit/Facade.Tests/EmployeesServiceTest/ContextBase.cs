namespace Linn.Stores.Facade.Tests.EmployeesServiceTest
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected EmployeesService Sut { get; private set; }

        protected IRepository<Employee, int> EmployeeRepository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.EmployeeRepository = Substitute.For<IRepository<Employee, int>>();
            this.Sut = new EmployeesService(this.EmployeeRepository);
        }
    }
}
