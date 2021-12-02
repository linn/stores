namespace Linn.Stores.Domain.LinnApps.Tests.MechPartSourceServiceTests
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdatingAndUserNotAuthorised : ContextBase
    {
        private MechPartSource from;

        private MechPartSource to;

        [SetUp]
        public void SetUp()
        {
            this.from = new MechPartSource { AssemblyType = "A" };
            this.to = new MechPartSource { AssemblyType = "B" };

            this.AuthorisationService.HasPermissionFor(Arg.Any<string>(), Arg.Any<IEnumerable<string>>()).Returns(false);
        }

        [Test]
        public void ShouldThrowException()
        {
            var ex = Assert.Throws<UpdatePartException>(() => this.Sut.Update(this.from, this.to, new List<string>()));
            ex.Message.Should().Be("You are not authorised to update.");
        }
    }
}
