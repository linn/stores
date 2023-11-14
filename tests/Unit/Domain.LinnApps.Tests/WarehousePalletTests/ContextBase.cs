namespace Linn.Stores.Domain.LinnApps.Tests.WarehousePalletTests
{
    using Linn.Stores.Domain.LinnApps.Wcs;
    using NUnit.Framework;

    public class ContextBase
    {
        protected WarehousePallet Sut { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.Sut = new WarehousePallet();
        }
    }
}
