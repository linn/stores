namespace Linn.Stores.Facade.Tests.ExportRsnService
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdatingExportReturn : ContextBase
    {
        private IResult<ExportReturn> result;

        [SetUp]
        public void SetUp()
        {
            var resource = new ExportReturnResource
                               {
                                   ReturnId = 22,
                                   ExportReturnDetails = new List<ExportReturnDetailResource>
                                                             {
                                                                 new ExportReturnDetailResource { RsnNumber = 123 }
                                                             }
                               };

            this.ExportReturnRepository.FindById(22).Returns(new ExportReturn { ReturnId = 22 });

            this.ExportReturnDetailRepository.FindById(Arg.Any<ExportReturnDetailKey>())
                .Returns(new ExportReturnDetail { RsnNumber = 123 });

            this.result = this.Sut.UpdateExportReturn(22, resource);
        }

        [Test]
        public void ShouldCallExportReturnRepository()
        {
            this.ExportReturnRepository.Received().FindById(22);
        }

        [Test]
        public void ShouldCallExportReturnDetailRepository()
        {
            this.ExportReturnDetailRepository.Received().FindById(Arg.Any<ExportReturnDetailKey>());
        }

        [Test]
        public void ShouldCommitResult()
        {
            this.TransactionManager.Received().Commit();
        }

        [Test]
        public void ShouldReturnExportReturn()
        {
            this.result.Should().BeOfType<SuccessResult<ExportReturn>>();
        }
    }
}