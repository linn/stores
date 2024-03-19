namespace Linn.Stores.Domain.LinnApps.Tests.LogisticsLabelServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenPrintingGeneralLabel : ContextBase
    {
        private ProcessResult result;

        [SetUp]
        public void SetUp()
        {
            this.result = this.Sut.PrintGeneralLabel(
                "line1",
                "line2",
                "line3",
                string.Empty,
                string.Empty,
                this.UserNumber);
        }

        [Test]
        public void ShouldPrintLabel()
        {
            this.BartenderLabelPack.Received().PrintLabels(
                $"GeneralLabel{23}",
                "DispatchLabels1",
                1,
                "dispatchgeneral.btw",
                Arg.Any<string>(),
                ref Arg.Any<string>());
        }

        [Test]
        public void ShouldReturnResult()
        {
            this.result.Message.Should().Be("1 general label(s) printed");
            this.result.Success.Should().BeTrue();
        }
    }
}
