namespace Linn.Stores.Domain.LinnApps.Tests.PartServiceTests
{
    using System.Collections.Generic;

    using FluentAssertions;
    using FluentAssertions.Extensions;

    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Domain.LinnApps.Sos;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenAddingPartAndUserHasPrivileges : ContextBase
    {
        private Part part;

        private Part result;

        private List<string> privileges;

        [SetUp]
        public void SetUp()
        {
            this.part = new Part();
           this.privileges = new List<string> { "part.admin" };

           this.AuthService.HasPermissionFor(AuthorisedAction.PartAdmin, this.privileges).Returns(true);

           this.result = this.Sut.CreatePart(this.part, this.privileges);
        }

        [Test]
        public void ShouldReturnNewPart()
        {
            this.result.Should().BeOfType<Part>();
        }
    }
}