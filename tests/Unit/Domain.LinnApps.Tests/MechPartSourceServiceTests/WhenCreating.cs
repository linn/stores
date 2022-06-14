namespace Linn.Stores.Domain.LinnApps.Tests.MechPartSourceServiceTests
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenCreating : ContextBase
    {
        private MechPartSource candidate;

        private MechPartSource result;

        [SetUp]
        public void SetUp()
        {
            this.candidate = new MechPartSource
                                 {
                                     Id = 1,
                                     SafetyCritical = "Y",
                                     SafetyDataDirectory = "/path",
                                     MechanicalOrElectrical = "M",
                                     Usages = new List<MechPartUsage>
                                                  {
                                                      new MechPartUsage { Product = "TEST" }
                                                  }
                                 };

            var userPrivileges = new List<string>();

            this.AuthorisationService.HasPermissionFor(Arg.Any<string>(), Arg.Any<IEnumerable<string>>()).Returns(true);
            this.result = this.Sut.Create(this.candidate, userPrivileges);
        }

        [Test]
        public void ShouldReturnItem()
        {
            this.result.Id.Should().Be(1);
        }
    }
}
