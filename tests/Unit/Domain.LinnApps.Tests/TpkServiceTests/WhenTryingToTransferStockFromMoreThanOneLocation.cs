namespace Linn.Stores.Domain.LinnApps.Tests.TpkServiceTests
{
    using System;
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.Tpk;
    using Linn.Stores.Domain.LinnApps.Tpk.Models;

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
            var ex = Assert.Throws<TpkException>(() => this.Sut.TransferStock(new TpkRequest
                                                                                  {
                                                                                      StockToTransfer = toTransfer,
                                                                                      DateTimeTpkViewQueried = DateTime.Now
                                                                                  }));
            ex.Message.Should()
                .Be("You can only TPK one pallet at a time");
        }
    }
}
