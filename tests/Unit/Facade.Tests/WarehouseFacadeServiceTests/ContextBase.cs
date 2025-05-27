namespace Linn.Stores.Facade.Tests.WarehouseFacadeServiceTests
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Wcs;
    using Linn.Stores.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected WarehouseFacadeService Sut { get; private set; }

        protected IWarehouseService WarehouseService { get;  private set; }

        protected IRepository<Employee, int> EmployeeRepository { get; private set; }

        protected IScsPalletsRepository ScsPalletsRepository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.WarehouseService = Substitute.For<IWarehouseService>();
            this.EmployeeRepository = Substitute.For<IRepository<Employee, int>>();
            this.ScsPalletsRepository = Substitute.For<IScsPalletsRepository>();
            this.Sut = new WarehouseFacadeService(this.WarehouseService, this.EmployeeRepository, this.ScsPalletsRepository);
        }
    }
}
