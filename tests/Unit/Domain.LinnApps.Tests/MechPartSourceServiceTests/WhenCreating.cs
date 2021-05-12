namespace Linn.Stores.Domain.LinnApps.Tests.MechPartSourceServiceTests
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.Parts;

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
                                 };

            this.result = this.Sut.Create(this.candidate, new List<PartDataSheet>());
        }

        [Test]
        public void ShouldReturnItem()
        {
            this.result.Id.Should().Be(1);
        }
    }
}
