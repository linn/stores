namespace Linn.Stores.Domain.LinnApps.Tests.ConsignmentServiceTests
{
    using System;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Domain.LinnApps.Exceptions;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingPackingListButNoConsignment : ContextBase
    {
        private Action action;

        [SetUp]
        public void SetUp()
        {
            this.ConsignmentRepository.FindById(this.ConsignmentId).Returns((Consignment)null);
            this.action = () => this.Sut.GetPackingList(this.ConsignmentId);
        }

        [Test]
        public void ShouldThrowException()
        {
            this.action.Should().Throw<NotFoundException>();
        }
    }
}
