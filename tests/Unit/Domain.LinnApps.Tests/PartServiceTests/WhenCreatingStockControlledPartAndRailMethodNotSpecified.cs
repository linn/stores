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

    public class WhenCreatingStockControlledPartAndRailMethodNotSpecified : ContextBase
    {
        private Part partToCreate;

        private List<string> privileges;

        [SetUp]
        public void SetUp()
        {
            this.partToCreate = new Part 
                                    { 
                                        StockControlled = "Y", 
                                        RawOrFinished = "R",
                                        QcOnReceipt = "N"
                                    };
            this.privileges = new List<string> { "part.admin" };
            this.TemplateRepository.FindById(Arg.Any<string>()).Returns(new PartTemplate());
            this.PartPack.PartRoot(Arg.Any<string>()).Returns("ROOT");
            this.AuthService.HasPermissionFor(AuthorisedAction.PartAdmin, this.privileges).Returns(true);

            this.Sut.CreatePart(this.partToCreate, this.privileges, false);
        }

        [Test]
        public void ShouldDefaultToPolicy()
        {
            this.partToCreate.RailMethod.Should().Be("POLICY");
        }
    }
}
