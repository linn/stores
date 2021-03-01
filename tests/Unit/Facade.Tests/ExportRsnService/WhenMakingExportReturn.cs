namespace Linn.Stores.Facade.Tests.ExportRsnService
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenMakingExportReturn : ContextBase
    {
        private IResult<MakeExportReturnResult> result;

        [SetUp]
        public void SetUp()
        {
            var rsns = new List<int> { 123, 456 };

            this.ExportReturnsPack.MakeExportReturn("123,456", "Y")
                .Returns(new MakeExportReturnResult { ExportReturnId = 111 });

            this.result = this.Sut.MakeExportReturn(rsns, true);
        }

        [Test]
        public void ShouldFormatInputs()
        {
            this.ExportReturnsPack.Received().MakeExportReturn("123,456", "Y");
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<MakeExportReturnResult>>();
        }
    }
}