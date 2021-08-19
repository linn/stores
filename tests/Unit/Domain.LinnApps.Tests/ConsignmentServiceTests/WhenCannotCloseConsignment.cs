namespace Linn.Stores.Domain.LinnApps.Tests.ConsignmentServiceTests
{
    using System;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenCannotCloseConsignment : ContextBase
    {
        private Action action;

        [SetUp]
        public void SetUp()
        {
            this.ConsignmentProxyService.CanCloseAllocation(this.ConsignmentId)
                .Returns(new ProcessResult(false, "not allowed"));
            this.action = () => this.Sut.CloseConsignment(this.Consignment, 123);
        }

        [Test]
        public void ShouldThrowException()
        {
            this.action.Should().Throw<ConsignmentCloseException>();
        }
    }
}
