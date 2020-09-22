namespace Linn.Stores.Domain.LinnApps.Tests.PartServiceTests
{
    using Linn.Common.Authorisation;
    using Linn.Stores.Domain.LinnApps.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected IPartService Sut { get; private set; }

        protected IAuthorisationService AuthService { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.AuthService = Substitute.For<IAuthorisationService>();
            this.Sut = new PartService(this.AuthService);
        }
    }
}