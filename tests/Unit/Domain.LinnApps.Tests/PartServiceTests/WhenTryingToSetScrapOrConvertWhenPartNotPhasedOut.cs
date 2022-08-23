namespace Linn.Stores.Domain.LinnApps.Tests.PartServiceTests
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.Parts;

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
            this.to = new Part
                          {
                              DatePhasedOut = null, 
                              ScrapOrConvert = "CONVERT",
                              RawOrFinished = "R",
                              QcOnReceipt = "N"
                          };
            this.privileges = new List<string>();
        }

        [Test]
        public void ShouldSetScrapOrConvertNull()
        {
            this.Sut.UpdatePart(this.from, this.to, this.privileges);
            this.to.ScrapOrConvert.Should().BeNull();
        }
    }
}
