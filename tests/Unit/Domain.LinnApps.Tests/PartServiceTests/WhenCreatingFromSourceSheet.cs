﻿namespace Linn.Stores.Domain.LinnApps.Tests.PartServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Linn.Stores.Domain.LinnApps.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenCreatingFromSourceSheet : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.PhoneList.FindBy(Arg.Any<Expression<Func<PhoneListEntry, bool>>>()).Returns(
                new PhoneListEntry { EmailAddress = "user1@linn.co.uk", User = new AuthUser { Name = "user1" } });

            this.PartRepository.FindBy(Arg.Any<Expression<Func<Part, bool>>>()).Returns(new Part { PartNumber = "PART" });
            this.SourceRepository.FindById(1).Returns(new MechPartSource { PartNumber = "PART", Id = 1 });
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
                "user1@linn.co.uk",
                "user1",
                Arg.Any<List<Dictionary<string, string>>>(),
                Arg.Is<List<Dictionary<string, string>>>(x => x == null),
                "stores@linn.co.uk",
                "Parts Utility",
                "New Source Sheet Created - PART",
                "Click here to view: https://app.linn.co.uk/parts/sources/1",
                null, 
                null);
        }
    }
}
