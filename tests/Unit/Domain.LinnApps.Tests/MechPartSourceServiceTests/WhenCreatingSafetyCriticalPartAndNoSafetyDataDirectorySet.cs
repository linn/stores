namespace Linn.Stores.Domain.LinnApps.Tests.MechPartSourceServiceTests
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.Parts;

    using NUnit.Framework;

    public class WhenCreatingSafetyCriticalPartAndNoSafetyDataDirectorySet : ContextBase
    {
        private MechPartSource candidate;

        [SetUp]
        public void SetUp()
        {
            this.candidate = new MechPartSource
                                 {
                                     SafetyCritical = "Y"
                                 };
        }

        [Test]
        public void ShouldThrowException()
        {
            var ex = Assert.Throws<CreatePartException>(() => this.Sut.Create(this.candidate, new List<PartDataSheet>()));
            ex.Message.Should().Be("You must enter a EMC/safety data directory for EMC or safety critical parts");
        }
    }
}
