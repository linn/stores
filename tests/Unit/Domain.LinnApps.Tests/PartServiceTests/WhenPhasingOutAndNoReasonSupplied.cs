namespace Linn.Stores.Domain.LinnApps.Tests.PartServiceTests
{
    using System;
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenPhasingOutAndNoReasonSupplied : ContextBase
    {
        private Part from;

        private Part to;

        private List<string> privileges;

        [SetUp]
        public void SetUp()
        {
            this.from = new Part { DatePhasedOut = null };
            this.to = new Part { DatePhasedOut = DateTime.Today };
            this.privileges = new List<string> { "part.admin" };

            this.AuthService.HasPermissionFor(AuthorisedAction.PartAdmin, this.privileges).Returns(true);
        }

        [Test]
        public void ShouldThrowException()
        {
            var ex = Assert.Throws<UpdatePartException>(() => this.Sut.UpdatePart(this.from, this.to, this.privileges, null));
            ex.Message.Should().Be("Must Provide a Reason When phasing out a part.");
        }
    }
}
