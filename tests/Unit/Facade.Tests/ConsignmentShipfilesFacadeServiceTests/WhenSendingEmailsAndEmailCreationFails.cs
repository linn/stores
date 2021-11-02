namespace Linn.Stores.Facade.Tests.ConsignmentShipfilesFacadeServiceTests
{
    using System;
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ConsignmentShipfiles;
    using Linn.Stores.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenSendingEmailsAndEmailCreationFails : ContextBase
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

            this.DomainService.SendEmails(Arg.Any<ConsignmentShipfile>())
                .Returns(
                x => new ConsignmentShipfile { ConsignmentId = 1 },
                x => new ConsignmentShipfile { ConsignmentId = 2 },
                x => throw new Exception("Something broke unexpectedly"));

            this.result = this.Sut.SendEmails(this.requestResource);
        }

        [Test]
        public void ShouldCallDomainServiceMethodCorrectNumberOfTimes()
        {
            this.DomainService.Received(3).SendEmails(Arg.Any<ConsignmentShipfile>());
        }

        [Test]
        public void ShouldCommitChangesForEmailsBeforeErroringEmail()
        {
            this.TransactionManager.Received(2).Commit();
        }

        [Test]
        public void ShouldReturnServerFailure()
        {
            this.result.Should().BeOfType<ServerFailureResult<IEnumerable<ConsignmentShipfile>>>();
            var dataResult = (ServerFailureResult<IEnumerable<ConsignmentShipfile>>)this.result;
            dataResult.Message.Should().Be("Unexpected error creating email for consignment 3: Something broke unexpectedly");
        }
    }
}
