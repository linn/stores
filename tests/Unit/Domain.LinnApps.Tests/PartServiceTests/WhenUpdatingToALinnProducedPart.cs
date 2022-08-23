namespace Linn.Stores.Domain.LinnApps.Tests.PartServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdatingToALinnProducedPart : ContextBase
    {
        private Part to;

        private List<string> privileges;

        [SetUp]
        public void SetUp()
        {
            this.to = new Part
                                    {
                                        LinnProduced = "Y",
                                        StockControlled = "N",
                                        RawOrFinished = "R",
                                        QcOnReceipt - "Y"
                                    };
            this.privileges = new List<string> { "part.admin" };
            this.AuthService.HasPermissionFor(AuthorisedAction.PartAdmin, this.privileges).Returns(true);
            this.SupplierRepo.FindBy(Arg.Any<Expression<Func<Supplier, bool>>>()).Returns(new Supplier { Id = 4415 });
            this.Sut.UpdatePart(new Part(), this.to, this.privileges, 33087);
        }

        [Test]
        public void ShouldDefaultLinn()
        {
            this.to.PreferredSupplier.Id.Should().Be(4415);
        }
    }
}
