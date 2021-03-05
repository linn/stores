namespace Linn.Stores.Domain.LinnApps.Tests.TpkServiceTests
{
    using Linn.Stores.Domain.LinnApps.Tpk;

    using NUnit.Framework;

    public class ContextBase
    {
        protected ITpkService Sut { get; set; }

        [SetUp]
        public void SetUpContext()
        {
            this.Sut = new TpkService();
        }
    }
}
