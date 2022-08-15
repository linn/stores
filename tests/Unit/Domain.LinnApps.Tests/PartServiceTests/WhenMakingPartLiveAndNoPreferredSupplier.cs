namespace Linn.Stores.Domain.LinnApps.Tests.PartServiceTests
{
    using System;
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenMakingPartLiveAndNoPreferredSupplier : ContextBase
    {
        private Part to;

        private List<string> privileges;

        private Exception result;

        [SetUp]
        public void SetUp()
        {
            this.to = new Part
                          {
                              PartNumber = "PART",
                              MadeLiveBy = new Employee(),
                              DateLive = DateTime.Now,
                              RawOrFinished = "R"
                          };
            this.privileges = new List<string> { "part.admin" };
            this.PartPack.PartLiveTest(Arg.Any<string>(), out var message).Returns(true);
            this.AuthService.HasPermissionFor(AuthorisedAction.PartAdmin, this.privileges).Returns(true);

            this.result = Assert.Throws<UpdatePartException>(() => this.Sut.UpdatePart(
                new Part(), this.to, this.privileges));
        }

        [Test]
        public void ShouldThrowException()
        {
            this.result.Should().BeOfType<UpdatePartException>();
            this.result.Message.Should()
                .Be("Cannot make live without a preferred supplier!");
        }
    }
}
