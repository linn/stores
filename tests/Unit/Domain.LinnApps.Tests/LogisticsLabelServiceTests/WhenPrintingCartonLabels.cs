namespace Linn.Stores.Domain.LinnApps.Tests.LogisticsLabelServiceTests
{
    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenPrintingCartonLabels : ContextBase
    {
        private ProcessResult result;

        private int firstCarton;

        private int? lastCarton;

        [SetUp]
        public void SetUp()
        {
            this.firstCarton = 2;
            this.lastCarton = 3;

            this.result = this.Sut.PrintCartonLabel(
                this.ConsignmentId,
                this.firstCarton,
                this.lastCarton,
                this.UserNumber);
        }

        [Test]
        public void ShouldGetConsignment()
        {
            this.ConsignmentRepository.Received().FindById(this.ConsignmentId);
        }

        [Test]
        public void ShouldPrintLabels()
        {
            this.BartenderLabelPack.Received().PrintLabels(
                $"CartonLabel{this.ConsignmentId}item{2}",
                "DispatchLabels1",
                1,
                "dispatchaddress.btw",
                Arg.Any<string>(),
                ref Arg.Any<string>());
            this.BartenderLabelPack.Received().PrintLabels(
                $"CartonLabel{this.ConsignmentId}item{3}",
                "DispatchLabels1",
                1,
                "dispatchaddress.btw",
                Arg.Any<string>(),
                ref Arg.Any<string>());
        }

        [Test]
        public void ShouldReturnResult()
        {
            this.result.Message.Should().Be("2 carton label(s) printed");
            this.result.Success.Should().BeTrue();
        }
    }
}
