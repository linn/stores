namespace Linn.Stores.Domain.LinnApps.Tests.WandServiceTests
{
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Wand;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected IWandService Sut { get; private set; }

        protected IWandPack WandPack { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.WandPack = Substitute.For<IWandPack>();
            this.Sut = new WandService(this.WandPack);
        }
    }
}
