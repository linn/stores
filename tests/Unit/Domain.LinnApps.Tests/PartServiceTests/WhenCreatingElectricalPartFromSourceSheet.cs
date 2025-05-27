namespace Linn.Stores.Domain.LinnApps.Tests.PartServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Common.Configuration;
    using Linn.Stores.Domain.LinnApps.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenCreatingElectricalPartFromSourceSheet : ContextBase
    {
        private Part result;

        [SetUp]
        public void SetUp()
        {
            this.PartRepository.FindBy(Arg.Any<Expression<Func<Part, bool>>>()).Returns(new Part { PartNumber = "PART" });
            this.SourceRepository.FindById(1).Returns(new MechPartSource { PartNumber = "PART", MechanicalOrElectrical = "E", Id = 1 });
            this.EmployeeRepository.FindById(33087).Returns(new Employee { FullName = "MONSIEUR ENGINEER" });
            this.PartPack.CreatePartFromSourceSheet(1, 33087, out var message).Returns(
                x =>
                    {
                        x[2] = "Created part PART";
                        return "PART";
                    });
            this.result = this.Sut.CreateFromSource(1, 33087, new List<PartDataSheet>());
        }

        [Test]
        public void ShouldSendEmail()
        {
            this.EmailService.Received().SendEmail(
                ConfigurationManager.Configuration["TEST_EMAIL"],
                "Electronic Sourcing Sheet",
                Arg.Is<List<Dictionary<string, string>>>(x => x == null),
                Arg.Is<List<Dictionary<string, string>>>(x => x == null),
                ConfigurationManager.Configuration["TEST_EMAIL"],
                "Parts Utility",
                "New Source Sheet Created - PART",
                "Click here to view: https://app.linn.co.uk/parts/sources/1",
                null, 
                null);
        }

        [Test]
        public void ShouldAddQcControl()
        {
            this.result.QcOnReceipt.Should().Be("Y");
            var expectedInfo = "NEW PART - MONSIEUR ENGINEER";
            this.result.QcInformation.Should().Be(expectedInfo);
            this.QcControlRepo.Received(1).Add(
                Arg.Is<QcControl>(x => x.Reason == expectedInfo));
        }
    }
}
