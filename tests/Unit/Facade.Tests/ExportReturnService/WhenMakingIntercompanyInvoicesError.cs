namespace Linn.Stores.Facade.Tests.ExportReturnService
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenMakingIntercompanyInvoicesError : ContextBase
    {
        private IResult<ExportReturn> result;

        private ExportReturnResource resource;

        [SetUp]
        public void SetUp()
        {
            this.resource = new ExportReturnResource
                                {
                                    ReturnId = 123
                                };

            this.ExportReturnsPack.MakeIntercompanyInvoices(123).Returns("not ok");

            this.ExportReturnRepository.FindById(123).Returns(new ExportReturn { ReturnId = 123 });

            this.result = this.Sut.MakeIntercompanyInvoices(this.resource);
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