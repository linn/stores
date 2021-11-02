namespace Linn.Stores.Facade.Tests.ConsignmentShipfilesFacadeServiceTests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ConsignmentShipfiles;
    using Linn.Stores.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenSendingEmails : ContextBase
    {
        private IResult<IEnumerable<ConsignmentShipfile>> result;

        private ConsignmentShipfilesSendEmailsRequestResource requestResource;

        [SetUp]
        public void SetUp()
        {
            this.requestResource = new ConsignmentShipfilesSendEmailsRequestResource
                                       {
                                           Shipfiles = new List<ConsignmentShipfileResource>
                                                           {
                                                               new ConsignmentShipfileResource { ConsignmentId = 1 },
                                                               new ConsignmentShipfileResource { ConsignmentId = 2 },
                                                               new ConsignmentShipfileResource { ConsignmentId = 3 },
                                                           }
            };

            this.DomainService.SendEmails(Arg.Any<ConsignmentShipfile>()).Returns(
                new ConsignmentShipfile { ConsignmentId = 1 },
                new ConsignmentShipfile { ConsignmentId = 2 },
                new ConsignmentShipfile { ConsignmentId = 3 });

            this.result = this.Sut.SendEmails(this.requestResource);
        }

        [Test]
        public void ShouldCallDomainServiceMethodCorrectNumberOfTimes()
        {
            this.DomainService.Received(3).SendEmails(Arg.Any<ConsignmentShipfile>());
        }

        [Test]
        public void ShouldCommitChanges()
        {
            this.TransactionManager.Received(3).Commit();
        }


        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<IEnumerable<ConsignmentShipfile>>>();
            var dataResult = (SuccessResult<IEnumerable<ConsignmentShipfile>>)this.result;
            dataResult.Data.Count().Should().Be(3);
            dataResult.Data.ElementAt(0).ConsignmentId.Should().Be(1);
            dataResult.Data.ElementAt(1).ConsignmentId.Should().Be(2);
            dataResult.Data.ElementAt(2).ConsignmentId.Should().Be(3);
        }
    }
}
