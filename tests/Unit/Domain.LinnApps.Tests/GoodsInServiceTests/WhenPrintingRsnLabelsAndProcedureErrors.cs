namespace Linn.Stores.Domain.LinnApps.Tests.GoodsInServiceTests
{
    using System;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Domain.LinnApps.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenPrintingRsnLabelsAndProcedureErrors : ContextBase
    {
        private ProcessResult result;

        [SetUp]
        public void SetUp()
        {
            this.LabelTypeRepository.FindBy(Arg.Any<Expression<Func<StoresLabelType, bool>>>())
                .Returns(new StoresLabelType
                             {
                                 Code = "RSN_LABEL",
                                 FileName = "template.ext",
                                 DefaultPrinter = "Printer"
                             });

            this.Bartender.When(b => b.PrintLabels(
                "RSN 123",
                "Printer",
                1,
                "template.ext",
                Arg.Any<string>(),
                ref Arg.Any<string>())).Do(
                x =>
                    {
                        x[5] = "The Printer isn't working";
                    });

            this.result = this.Sut.PrintRsnLabels(
                123,
                "PART",
                5678,
                "Printer");
        }

        [Test]
        public void ShouldCallBartenderWithCorrectParameters()
        {
            this.Bartender.Received(1).PrintLabels(
                "RSN 123",
                "Printer",
                1,
                "template.ext",
                "\"123\",\"PART\",\"5678\"",
                ref Arg.Any<string>());
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Success.Should().BeFalse();
            this.result.Message.Should().Be("The Printer isn't working");
        }
    }
}
