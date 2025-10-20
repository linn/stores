namespace Linn.Stores.Domain.LinnApps.Tests.ConsignmentServiceTests
{
    using System;
    using System.Threading.Tasks;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenCannotCloseConsignment : ContextBase
    {
        private Func<Task> action;

        [SetUp]
        public void SetUp()
        {
            this.ConsignmentProxyService.CanCloseAllocation(this.ConsignmentId)
                .Returns(new ProcessResult(false, "not allowed"));

            this.action = async () => await this.Sut.CloseConsignment(this.Consignment, 123);
        }

        [Test]
        public async Task ShouldThrowException()
        {
            await this.action.Should().ThrowAsync<ConsignmentCloseException>();
        }
    }
}
