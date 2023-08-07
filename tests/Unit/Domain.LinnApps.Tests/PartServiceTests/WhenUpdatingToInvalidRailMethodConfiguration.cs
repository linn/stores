namespace Linn.Stores.Domain.LinnApps.Tests.PartServiceTests
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.Parts;

    using NUnit.Framework;

    public class WhenUpdatingToInvalidRailMethodConfiguration : ContextBase
    {
        private Part from;

        private Part to;

        private List<string> privileges;

        [SetUp]
        public void SetUp()
        {
            this.from = new Part();
            this.to = new Part
                          {
                              RailMethod = "SMM",
                              StockControlled = "Y",
                              MinStockRail = 0,
                              MaxStockRail = 0
                          };
            this.privileges = new List<string> { "part.admin" };
        }

        [Test]
        public void ShouldThrowException()
        {
            var ex = Assert.Throws<UpdatePartException>(() => this.Sut.UpdatePart(this.from, this.to, this.privileges, 33087));
            ex.Message.Should().Be("Rail method SMM with 0 min/max rails is not a valid stocking policy.");
        }
    }
}