namespace Linn.Stores.Domain.LinnApps.Tests.ConsignmentServiceTests
{
    using System;
    using System.Threading.Tasks;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Exceptions;

    using NUnit.Framework;

    public class WhenClosingButNotOpen : ContextBase
    {
        private Func<Task> action;

        [SetUp]
        public void SetUp()
        {
            this.Consignment.Status = "C";
            this.action = async () => await this.Sut.CloseConsignment(this.Consignment, 123);
        }

        [Test]
        public async Task ShouldThrowException()
        {
            await this.action.Should().ThrowAsync<ConsignmentCloseException>();
        }
    }
}
