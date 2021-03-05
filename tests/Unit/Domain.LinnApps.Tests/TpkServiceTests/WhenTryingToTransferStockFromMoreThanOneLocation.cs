namespace Linn.Stores.Domain.LinnApps.Tests.TpkServiceTests
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.Tpk;

    using NUnit.Framework;

    public class WhenTryingToTransferStockFromMoreThanOneLocation : ContextBase
    {
        [Test]
        public void ShouldThrowException()
        {
            var toTransfer = new List<TransferableStock>
                                 {
                                     new TransferableStock { FromLocation = "A" },
                                     new TransferableStock { FromLocation = "B" }
                                 };
            var ex = Assert.Throws<TpkException>(() => this.Sut.TransferStock(toTransfer));
            ex.Message.Should()
                .Be("You can only TPK one pallet at a time");
        }
    }
}
