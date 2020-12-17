namespace Linn.Stores.Domain.LinnApps.Tests.MechPartSourceServiceTests
{
    using Linn.Stores.Domain.LinnApps.Parts;

    using NUnit.Framework;

    public class ContextBase
    {
        protected IMechPartSourceService Sut { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.Sut = new MechPartSourceService();
        }
    }
}
