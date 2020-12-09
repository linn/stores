namespace Linn.Stores.Domain.LinnApps.Tests.PartServiceTests
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenCreatingFromSourceSheetAndProxiedServiceErrors : ContextBase
    {
        private List<string> privileges;

        [SetUp]
        public void SetUp()
        {
            this.privileges = new List<string> { "irrelevant.privilege" };

            this.AuthService.HasPermissionFor(AuthorisedAction.PartAdmin, this.privileges).Returns(false);
            this.SourceRepository.FindById(Arg.Any<int>()).Returns(new MechPartSource { });
            this.PartPack.CreatePartFromSourceSheet(Arg.Any<int>(), Arg.Any<int>(), out Arg.Any<string>())
                .Returns(
                    x =>
                        {
                            x[2] = "A Message other than the Success Message"; 
                            return string.Empty;
                        });
        }

        [Test]
        public void ShouldThrowException()
        {
            var ex = Assert.Throws<CreatePartException>(() => this.Sut.CreateFromSource(1, 1));
            ex.Message.Should().Be("A Message other than the Success Message");
        }
    }
}
