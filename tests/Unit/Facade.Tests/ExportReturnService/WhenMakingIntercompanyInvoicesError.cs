namespace Linn.Stores.Facade.Tests.ExportReturnService
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;

    using NUnit.Framework;

    public class WhenMakingIntercompanyInvoicesError : ExportRsnService.ContextBase
    {
        private IResult<ExportReturn> result;

        [SetUp]
        public void SetUp()
        {
            this.ExportReturnsPack.MakeIntercompanyInvoices(123).Returns("not ok");

            this.ExportReturnRepository.FindById(123).Returns(new ExportReturn { ReturnId = 123 });

            this.result = this.Sut.MakeIntercompanyInvoices(123);
        }

        [Test]
        public void ShouldCallPackage()
        {
            this.ExportReturnsPack.Received().MakeIntercompanyInvoices(123);
        }

        [Test]
        public void ShouldReturnExportReturn()
        {
            this.result.Should().BeOfType<BadRequestResult<ExportReturn>>();
        }
    }
}