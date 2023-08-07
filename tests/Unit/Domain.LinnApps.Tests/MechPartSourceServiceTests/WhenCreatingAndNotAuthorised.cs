namespace Linn.Stores.Domain.LinnApps.Tests.MechPartSourceServiceTests
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenCreatingAndNotAuthorised : ContextBase
    {
        private MechPartSource candidate;

        [SetUp]
        public void SetUp()
        {
            this.candidate = new MechPartSource { AssemblyType = "B" };

            this.AuthorisationService.HasPermissionFor(
                AuthorisedAction.ParcelAdmin, Arg.Any<IEnumerable<string>>()).Returns(false);
        }

        [Test]
        public void ShouldThrowException()
        {
            var ex = Assert.Throws<CreatePartException>(() => this.Sut.Create(this.candidate, new List<string>()));
            ex.Message.Should().Be("You are not authorised to create.");
        }
    }
}
