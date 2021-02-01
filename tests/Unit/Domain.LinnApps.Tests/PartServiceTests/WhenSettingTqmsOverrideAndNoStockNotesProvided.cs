namespace Linn.Stores.Domain.LinnApps.Tests.PartServiceTests
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.Parts;

    using NUnit.Framework;

    public class WhenSettingTqmsOverrideAndNoStockNotesProvided : ContextBase
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
                              TqmsCategoryOverride = "override"
                          };
            this.privileges = new List<string> { "part.admin" };
        }

        [Test]
        public void ShouldThrowException()
        {
            var ex = Assert.Throws<UpdatePartException>(() => this.Sut.UpdatePart(this.from, this.to, this.privileges, null));
            ex.Message.Should().Be("You must enter a reason and/or reference or project code when setting an override");
        }
    }
}