﻿namespace Linn.Stores.Domain.LinnApps.Tests.PartServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Linn.Common.Configuration;
    using Linn.Stores.Domain.LinnApps.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenCreatingMechanicalPartFromSourceSheet : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.PartRepository.FindBy(Arg.Any<Expression<Func<Part, bool>>>()).Returns(new Part { PartNumber = "PART" });
            this.SourceRepository.FindById(1).Returns(new MechPartSource { PartNumber = "PART", MechanicalOrElectrical = "M", Id = 1 });
            this.PartPack.CreatePartFromSourceSheet(1, 33087, out var message).Returns(
                x =>
                    {
                        x[2] = "Created part PART";
                        return "PART";
                    });
            this.Sut.CreateFromSource(1, 33087, new List<PartDataSheet>());
        }

        [Test]
        public void ShouldSendEmail()
        {
            this.EmailService.Received().SendEmail(
                ConfigurationManager.Configuration["TEST_EMAIL"],
                "Mechanical Sourcing Sheet",
                Arg.Is<List<Dictionary<string, string>>>(x => x == null),
                Arg.Is<List<Dictionary<string, string>>>(x => x == null),
                ConfigurationManager.Configuration["TEST_EMAIL"],
                "Parts Utility",
                "New Source Sheet Created - PART",
                "Click here to view: https://app.linn.co.uk/parts/sources/1",
                null, 
                null);
        }
    }
}
