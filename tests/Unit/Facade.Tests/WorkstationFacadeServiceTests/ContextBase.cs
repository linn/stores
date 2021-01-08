namespace Linn.Stores.Facade.Tests.WorkstationFacadeServiceTests
{
    using Linn.Stores.Domain.LinnApps.Workstation;
    using Linn.Stores.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected WorkstationFacadeService Sut { get; private set; }

        protected IWorkstationService WorkstationService { get;  private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.WorkstationService = Substitute.For<IWorkstationService>();
            this.Sut = new WorkstationFacadeService(this.WorkstationService);
        }
    }
}
