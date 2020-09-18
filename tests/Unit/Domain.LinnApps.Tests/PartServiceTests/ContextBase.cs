namespace Linn.Stores.Domain.LinnApps.Tests.PartServiceTests
{
    using Linn.Common.Authorisation;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected IPartService Sut { get; private set; }

        protected IAuthorisationService AuthService { get; private set; }

        protected IQueryRepository<Supplier> SupplierRepo { get; set; }

        protected IRepository<QcControl, int> qcControlRepo { get; set; }

        [SetUp]
        public void SetUpContext()
        {
            this.AuthService = Substitute.For<IAuthorisationService>();
            this.SupplierRepo = Substitute.For<IQueryRepository<Supplier>>();
            this.qcControlRepo = Substitute.For<IRepository<QcControl, int>>();
            this.Sut = new PartService(this.AuthService, this.qcControlRepo, this.SupplierRepo);
        }
    }
}