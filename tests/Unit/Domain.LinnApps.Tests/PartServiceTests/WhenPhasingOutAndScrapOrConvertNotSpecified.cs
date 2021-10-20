namespace Linn.Stores.Domain.LinnApps.Tests.PartServiceTests
{
    using System;
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenPhasingOutAndScrapOrConvertNotSpecified : ContextBase
    {
        private Part from;

        private Part to;

        private List<string> privileges;

        [SetUp]
        public void SetUp()
        {
            this.from = new Part { DatePhasedOut = null };
            this.to = new Part { DatePhasedOut = DateTime.Today, ReasonPhasedOut = "reason", ScrapOrConvert = null };
            this.privileges = new List<string> { "part.admin" };
            this.AuthService.HasPermissionFor(AuthorisedAction.PartAdmin, this.privileges).Returns(true);

            this.Sut.UpdatePart(this.from, this.to, this.privileges, null);
        }

        [Test]
        public void ShouldDefaultToConvert()
        {
            this.from.ScrapOrConvert.Should().Be("CONVERT");
        }
    }
}
