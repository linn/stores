namespace Linn.Stores.Domain.LinnApps.Tests.WarehouseLocationTests
{
    using Linn.Stores.Domain.LinnApps.Wcs;
    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected WarehouseLocation Sut { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.Sut = new WarehouseLocation();
        }
    }
}
