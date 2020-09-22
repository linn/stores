namespace Linn.Stores.Domain.LinnApps.Tests.PartServiceTests
{
    using System;
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenTryingToSetScrapOrConvertWhenPartNotPhasedOut : ContextBase
    {
        private Part from;

        private Part to;

        private List<string> privileges;

        [SetUp]
        public void SetUp()
        {
            this.from = new Part { DatePhasedOut = null };
            this.to = new Part { DatePhasedOut = null, ScrapOrConvert = "CONVERT" };
            this.privileges = new List<string> { };
        }

        [Test]
        public void ShouldThrowException()
        {
            var ex = Assert.Throws<UpdatePartException>(() => this.Sut.UpdatePart(this.from, this.to, this.privileges));
            ex.Message.Should().Be("A part must be obsolete to be convertible or to be scrapped.");
        }
    }
}