namespace Linn.Stores.Service.Tests.CurrencyModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingAll : ContextBase
    {
        private Currency currency1;

        private Currency currency2;

        [SetUp]
        public void SetUp()
        {
            this.currency1 = new Currency { Code = "GBP", Name = "Pounds sterling" };
            this.currency2 = new Currency { Code = "EUR", Name = "Euros" };

            this.CurrencyFacadeService.GetAll().Returns(
                new SuccessResult<IEnumerable<Currency>>(new List<Currency> { this.currency1, this.currency2 }));

            this.Response = this.Browser.Get(
                "/logistics/currencies",
                with => { with.Header("Accept", "application/json"); }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.CurrencyFacadeService.Received().GetAll();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<IEnumerable<CurrencyResource>>().ToList();
            resultResource.Should().HaveCount(2);
            resultResource.FirstOrDefault(a => a.Code == this.currency1.Code && a.Name == this.currency1.Name).Should()
                .NotBeNull();
            resultResource.FirstOrDefault(a => a.Code == this.currency2.Code && a.Name == this.currency2.Name).Should()
                .NotBeNull();
        }
    }
}
