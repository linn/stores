namespace Linn.Stores.Domain.LinnApps.Tests.LogisticsLabelServiceTests
{
    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Models;

    using NUnit.Framework;

    public class WhenPrintingNonExistentCartonLabel : ContextBase
    {
        private int firstCarton;

        private int lastCarton;

        private ProcessResult result;

        [SetUp]
        public void SetUp()
        {
            this.firstCarton = 1;
            this.lastCarton = 23213;

            this.result = this.Sut.PrintCartonLabel(
                this.ConsignmentId,
                this.firstCarton,
                this.lastCarton,
                this.UserNumber,
                1);
        }

        [Test]
        public void ShouldReturnFailureResult()
        {
            this.result.Message.Should().Be("Printing Failed. Could not find carton 1 on consignment 808");
            this.result.Success.Should().BeFalse();
        }
    }
}
