namespace Linn.Stores.Domain.LinnApps.Tests.PartServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenCreatingAndPartNumberHasLeadingTrailingWhitespace : ContextBase
    {
        private Part part;

        private Part result;

        private List<string> privileges;

        [SetUp]
        public void SetUp()
        {
            this.part = new Part
            {
                PartNumber = " CAP 431 ",
                StockControlled = "N",
                RawOrFinished = "R",
                QcOnReceipt = "N"
            };
            this.privileges = new List<string> { "part.admin" };

            this.AuthService.HasPermissionFor(AuthorisedAction.PartAdmin, this.privileges).Returns(true);
            this.result = this.Sut.CreatePart(this.part, this.privileges, false);
        }

        [Test]
        public void ShouldTrimWhitespace()
        {
            this.result.PartNumber.Should().Be("CAP 431");
        }
    }
}
