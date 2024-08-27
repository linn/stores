namespace Linn.Stores.Domain.LinnApps.Tests.MechPartSourceServiceTests
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenCreatingAndNoProjectCode : ContextBase
    {
        private MechPartSource candidate;

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

            this.AuthorisationService.HasPermissionFor(Arg.Any<string>(), Arg.Any<IEnumerable<string>>()).Returns(true);
        }

        [Test]
        public void ShouldThrowException()
        {
            var ex = Assert.Throws<CreatePartException>(() => this.Sut.Create(this.candidate, new List<string>()));
            ex.Message.Should().Be("You must enter a project code when creating a source sheet");
        }
    }
}
