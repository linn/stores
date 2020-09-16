namespace Linn.Stores.Domain.LinnApps.Tests.PartServiceTests
{
    using System.Collections.Generic;

    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Domain.LinnApps.Sos;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenAddingPartAndUserDoesNotHavePrivileges : ContextBase
    {
        private Part part;

        private List<string> privileges;

        [SetUp]
        public void SetUp()
        {
            this.part = new Part();
            this.privileges = new List<string> { "irrelevant.privilege" };

            this.AuthService.HasPermissionFor(AuthorisedAction.PartAdmin, this.privileges).Returns(false);
        }

        [Test]
        public void ShouldThrowException()
        {
            var ex = Assert.Throws<UpdatePartException>(() => this.Sut.CreatePart(this.part, this.privileges));
            ex.Message.Should().Be("You are not authorised to create parts.");
        }
    }
}