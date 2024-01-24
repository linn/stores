namespace Linn.Stores.Domain.Tests.ScsPalletTests
{
    using Linn.Stores.Domain.LinnApps.Scs;
    using NUnit.Framework;

    public class ContextBase
    {
        protected ScsPallet Sut { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.Sut = new ScsPallet();
        }
    }
}
