namespace Linn.Stores.Domain.LinnApps.Tests.ConsignmentServiceTests
{
    using System;
    using System.Threading.Tasks;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenCannotInvoiceConsignment : ContextBase
    {
        private Action action;

        [SetUp]
        public void SetUp()
        {
            this.ConsignmentProxyService.CanCloseAllocation(this.ConsignmentId)
                .Returns(new ProcessResult(true, "ok"));
            this.InvoicingPack.InvoiceConsignment(this.ConsignmentId, 123)
                .Returns(new ProcessResult(false, "failed"));

            this.action = () => this.Sut.CloseConsignment(this.Consignment, 123);
        }

        [Test]
        public void ShouldThrowException()
        { 
            this.action.Should().Throw<ConsignmentCloseException>();
        }
    }
}
