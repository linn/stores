namespace Linn.Stores.Domain.LinnApps.Tests.PartServiceTests
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenCreatingAndCreationOfPartRootNoLongerAllowed : ContextBase
    {
        private Part part;

        private List<string> privileges;

        [SetUp]
        public void SetUp()
        {
            this.part = new Part { PartNumber = "CAP 431", StockControlled = "N" };
            this.privileges = new List<string> { "part.admin" };
            this.PartPack.PartRoot("CAP 431").Returns("CAP");
            this.AuthService.HasPermissionFor(AuthorisedAction.PartAdmin, this.privileges).Returns(true);
            this.TemplateRepository.FindById(Arg.Any<string>()).Returns(new PartTemplate { AllowPartCreation = "N" });
        }

        [Test]
        public void ShouldThrowException()
        {
            var ex = Assert.Throws<CreatePartException>(() => this.Sut.CreatePart(this.part, this.privileges));
            ex.Message.Should().Be("The system no longer allows creation of CAP parts.");
        }
    }
}