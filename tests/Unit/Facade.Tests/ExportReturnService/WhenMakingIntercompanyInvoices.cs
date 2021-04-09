namespace Linn.Stores.Facade.Tests.ExportReturnService
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenMakingIntercompanyInvoices : ContextBase
    {
        private IResult<ExportReturn> result;

        [SetUp]
        public void SetUp()
        {
            this.ExportReturnsPack.MakeIntercompanyInvoices(123).Returns("ok");

            this.ExportReturnRepository.FindById(123).Returns(new ExportReturn { ReturnId = 123 });

            this.result = this.Sut.MakeIntercompanyInvoices(123);
        }

        [Test]
        public void ShouldCallPackage()
        {
            this.ExportReturnsPack.Received().MakeIntercompanyInvoices(123);
        }

        [Test]
        public void ShouldCallRepository()
        {
            this.ExportReturnRepository.Received().FindById(123);
        }

        [Test]
        public void ShouldReturnExportReturn()
        {
            this.result.Should().BeOfType<SuccessResult<ExportReturn>>();
        }
    }
}