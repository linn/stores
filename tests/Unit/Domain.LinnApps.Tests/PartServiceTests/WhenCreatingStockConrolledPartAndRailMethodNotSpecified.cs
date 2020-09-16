namespace Linn.Stores.Domain.LinnApps.Tests.PartServiceTests
{
    using System;
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenCreatingStockConrolledPartAndRailMethodNotSpecified : ContextBase
    {
        private Part partToCreate;

        private List<string> privileges;

        [SetUp]
        public void SetUp()
        {
            this.partToCreate = new Part { StockControlled = "Y" };
            this.privileges = new List<string> { "part.admin" };
            this.AuthService.HasPermissionFor(AuthorisedAction.PartAdmin, this.privileges).Returns(true);

            this.Sut.CreatePart(this.partToCreate, this.privileges);
        }

        [Test]
        public void ShouldDefaultToPolicy()
        {
            this.partToCreate.RailMethod.Should().Be("POLICY");
        }
    }
}