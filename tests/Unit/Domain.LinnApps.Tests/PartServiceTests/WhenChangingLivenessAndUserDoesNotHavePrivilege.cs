namespace Linn.Stores.Domain.LinnApps.Tests.PartServiceTests
{
    using System;
    using System.Collections.Generic;

    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenChangingLivenessAndUserDoesNotHavePrivilege : ContextBase
    {
        private Part to;

        private List<string> privileges;

        [SetUp]
        public void SetUp()
        {
            this.to = new Part
                          {
                              PartNumber = "PART",
                              MadeLiveBy = new Employee(),
                              DateLive = DateTime.Now
                          };
            this.privileges = new List<string> { "part.admin" };
            this.PartPack.PartLiveTest(Arg.Any<string>(), out var message).Returns(true);
            this.AuthService.HasPermissionFor(AuthorisedAction.PartAdmin, this.privileges).Returns(false);
        }

        [Test]
        public void ShouldThrowException()
        {
            var ex = Assert.Throws<UpdatePartException>(() => this.Sut.UpdatePart(new Part(), this.to, this.privileges));
        }
    }
}