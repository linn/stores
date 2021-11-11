namespace Linn.Stores.Domain.LinnApps.Tests.MechPartSourceServiceTests
{
    using Linn.Common.Authorisation;
    using Linn.Stores.Domain.LinnApps.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected IMechPartSourceService Sut { get; private set; }

        protected IAuthorisationService AuthorisationService { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.AuthorisationService = Substitute.For<IAuthorisationService>();
            this.Sut = new MechPartSourceService(this.AuthorisationService);
        }
    }
}
