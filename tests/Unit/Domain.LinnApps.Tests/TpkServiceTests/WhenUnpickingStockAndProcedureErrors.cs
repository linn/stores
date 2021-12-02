namespace Linn.Stores.Domain.LinnApps.Tests.TpkServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Domain.LinnApps.Requisitions;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUnpickingStockAndProcedureErrors : ContextBase
    {
        private readonly IQueryable<ReqMove> moves = new List<ReqMove>
                                                         {
                                                             new ReqMove { Sequence = 1, Quantity = 1m, DateCancelled = null, StockLocatorId = 1 },
                                                             new ReqMove { Sequence = 2, Quantity = 1m, DateCancelled = null, StockLocatorId = 1 },
                                                             new ReqMove { Sequence = 3, Quantity = 1m, DateCancelled = null, StockLocatorId = 1 }
                                                         }.AsQueryable();
        
        private ProcessResult result;

        [SetUp]
        public void SetUp()
        {
            this.ReqMovesRepository.FilterBy(Arg.Any<Expression<Func<ReqMove, bool>>>())
                .Returns(this.moves.AsQueryable());

            this.StoresPack.UnpickStock(1, 1, Arg.Is<int>(x => x != 2), 1, 1, 1m, 1, 1)
                .Returns(new ProcessResult(true, string.Empty));

            this.StoresPack.UnpickStock(1, 1, Arg.Is<int>(x => x == 2), 1, 1, 1m, 1, 1)
                .Returns(new ProcessResult(false, "Fell at the second hurdle"));

            this.result = this.Sut.UnpickStock(1, 1, 1, 1, 1, 1, 1);
        }

        [Test]
        public void ShouldCallPack()
        {
            this.StoresPack.Received(2).UnpickStock(1, 1, Arg.Any<int>(), 1, 1, 1m, 1, 1);
        }

        [Test]
        public void ShouldNotCancelMoves()
        {
            this.moves.All(x => x.DateCancelled == null).Should().BeTrue();
        }

        [Test]
        public void ShouldReturnFail()
        {
            this.result.Success.Should().BeFalse();
            this.result.Message.Should().Be("Fell at the second hurdle");
        }
    }
}
