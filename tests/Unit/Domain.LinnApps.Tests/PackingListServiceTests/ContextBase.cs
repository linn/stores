namespace Linn.Stores.Domain.LinnApps.Tests.PackingListServiceTests
{
    using Linn.Stores.Domain.LinnApps.ConsignmentShipfiles;

    using NUnit.Framework;

    public class ContextBase
    {
        protected IPackingListService Sut { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.Sut = new PackingListService();
        }
    }
}
