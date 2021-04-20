namespace Linn.Stores.Facade.Tests.ExportReturnService
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenMakingIntercompanyInvoices : ContextBase
    {
        private IResult<ExportReturn> result;

        private ExportReturnResource resource;

        [SetUp]
        public void SetUp()
        {
            this.resource = new ExportReturnResource
                                {
                                    ReturnId = 123,
                                    ExportReturnDetails = new List<ExportReturnDetailResource>
                                                              {
                                                                  new ExportReturnDetailResource { RsnNumber = 123 }
                                                              }
                                };

            this.ExportReturnRepository.FindById(22).Returns(new ExportReturn { ReturnId = 22 });

            this.ExportReturnDetailRepository.FindById(Arg.Any<ExportReturnDetailKey>())
                .Returns(new ExportReturnDetail { RsnNumber = 123 });

            this.ExportReturnsPack.MakeIntercompanyInvoices(123).Returns("ok");

            this.ExportReturnRepository.FindById(123).Returns(new ExportReturn { ReturnId = 123 });

            this.result = this.Sut.MakeIntercompanyInvoices(this.resource);
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