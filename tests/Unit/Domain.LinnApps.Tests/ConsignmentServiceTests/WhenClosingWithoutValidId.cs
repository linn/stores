namespace Linn.Stores.Domain.LinnApps.Tests.ConsignmentServiceTests
{
    using System;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Exceptions;

    using NUnit.Framework;

    public class WhenClosingWithoutValidId : ContextBase
    {
        private Action action;

        [SetUp]
        public void SetUp()
        {
            this.action = () => this.Sut.CloseConsignment(this.Consignment, 0);
        }

        [Test]
        public void ShouldThrowException()
        {
            this.action.Should().Throw<ConsignmentCloseException>();
        }
    }
}
