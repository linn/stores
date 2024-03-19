namespace Linn.Stores.Domain.LinnApps.Tests.LogisticsLabelServiceTests
{
    using Linn.Stores.Domain.LinnApps.Models;
    using NSubstitute;
    using NUnit.Framework;

    using FluentAssertions;

    public class WhenPrintingAddressLabel : ContextBase
    {
        private ProcessResult result;

        private int addressId;

        private int? lastCarton;

        [SetUp]
        public void SetUp()
        {
            this.addressId = 1;
            this.lastCarton = 2;

            var address = new Address()
                              {
                                  Addressee = "Kate",
                                  Line1 = "Windsor Farm Shop",
                                  Line2 = "Windsor",
                                  Country = new Country { DisplayName = "England" }
                              };
            this.AddressRepository.FindById(this.addressId).Returns(address);

            this.result = this.Sut.PrintAddressLabel(
                this.addressId,
                this.UserNumber);
        }

        [Test]
        public void ShouldGetAddress()
        {
            this.AddressRepository.Received().FindById(this.addressId);
        }

        [Test]
        public void ShouldPrintLabel()
        {
            this.BartenderLabelPack.Received().PrintLabels(
                $"AddressLabel{this.addressId}",
                "DispatchLabels1",
                1,
                "dispatchaddress.btw",
                Arg.Any<string>(),
                ref Arg.Any<string>());
        }

        [Test]
        public void ShouldReturnResult()
        {
            this.result.Message.Should().Be("1 address label(s) printed");
            this.result.Success.Should().BeTrue();
        }
    }
}
