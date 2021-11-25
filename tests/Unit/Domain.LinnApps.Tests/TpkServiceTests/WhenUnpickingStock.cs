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

    public class WhenUnpickingStock : ContextBase
    {
        private ProcessResult result;

        private IQueryable<ReqMove> moves = new List<ReqMove>
                                                        {
                                                            new ReqMove { Sequence = 1, Quantity = 1m, DateCancelled = null },
                                                            new ReqMove { Sequence = 2, Quantity = 1m, DateCancelled = null },
                                                            new ReqMove { Sequence = 3, Quantity = 1m, DateCancelled = null }
                                                        }.AsQueryable();

        [SetUp]
        public void SetUp()
        {
            this.ReqMovesRepository.FilterBy(Arg.Any<Expression<Func<ReqMove, bool>>>())
                .Returns(this.moves.AsQueryable());

            this.StoresPack.UnpickStock(1, 1, Arg.Any<int>(), 1, 1, 1m, 1, 1)
                .Returns(new ProcessResult(true, string.Empty));

            this.result = this.Sut.UnpickStock(1, 1, 1, 1, 1, 1, 1, 1);
        }

        [Test]
        public void ShouldCallPack()
        {
            this.StoresPack.Received(3).UnpickStock(1, 1, Arg.Any<int>(), 1, 1, 1m, 1, 1);
        }

        [Test]
        public void ShouldCancelMoves()
        {
            this.moves.All(x => x.DateCancelled.HasValue).Should().BeTrue();
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Success.Should().BeTrue();
        }
    }
}
