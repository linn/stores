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

    public class WhenCreatingLinnProducedPartAndPreferredSupplierNotSpecified : ContextBase
    {
        private Part partToCreate;

        private List<string> privileges;

        [SetUp]
        public void SetUp()
        {
            this.partToCreate = new Part { LinnProduced = "Y", StockControlled = "N" };
            this.privileges = new List<string> { "part.admin" };
            this.TemplateRepository.FindById(Arg.Any<string>()).Returns(new PartTemplate());
            this.PartPack.PartRoot(Arg.Any<string>()).Returns("ROOT");
            this.AuthService.HasPermissionFor(AuthorisedAction.PartAdmin, this.privileges).Returns(true);
            this.SupplierRepo.FindBy(Arg.Any<Expression<Func<Supplier, bool>>>()).Returns(new Supplier { Id = 4415 });
            this.Sut.CreatePart(this.partToCreate, this.privileges, false);
        }

        [Test]
        public void ShouldDefaultLinn()
        {
            this.partToCreate.PreferredSupplier.Id.Should().Be(4415);
        }
    }
}