namespace Linn.Stores.Domain.LinnApps.Tests.PartServiceTests
{
    using System;
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenPhasingInAndUserDoesNotHavePermission : ContextBase
    {
        private Part from;

        private Part to;

        private List<string> privileges;

        [SetUp]
        public void SetUp()
        {
            this.from = new Part { DatePhasedOut = DateTime.UnixEpoch };
            this.to = new Part { DatePhasedOut = null };
            this.privileges = new List<string> { "irrelevant.privilege" };

            this.AuthService.HasPermissionFor(AuthorisedAction.PartAdmin, this.privileges).Returns(false);
        }

        [Test]
        public void ShouldThrowException()
        {
            var ex = Assert.Throws<UpdatePartException>(() => this.Sut.UpdatePart(this.from, this.to, this.privileges));
            ex.Message.Should().Be("You are not authorised to phase in parts.");
        }
    }
}
