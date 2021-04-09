namespace Linn.Stores.Facade.Tests.ExportReturnService
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenMakingExportReturn : ContextBase
    {
        private IResult<ExportReturn> result;

        [SetUp]
        public void SetUp()
        {
            var rsns = new List<int> { 123, 456 };

            this.ExportReturnsPack.MakeExportReturn("123,456", "Y")
                .Returns(111);

            this.ExportReturnRepository.FindById(111).Returns(new ExportReturn { ReturnId = 111 });

            this.result = this.Sut.MakeExportReturn(rsns, true);
        }

        [Test]
        public void ShouldFormatInputs()
        {
            this.ExportReturnsPack.Received().MakeExportReturn("123,456", "Y");
        }

        [Test]
        public void ShouldCallExportReturnsRepository()
        {
            this.ExportReturnRepository.Received().FindById(111);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<ExportReturn>>();
        }
    }
}