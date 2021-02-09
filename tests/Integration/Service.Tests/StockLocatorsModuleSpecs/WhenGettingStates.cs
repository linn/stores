namespace Linn.Stores.Service.Tests.StockLocatorsModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingStates : ContextBase
    {
        private InspectedState s1;

        private InspectedState s2;

        [SetUp]
        public void SetUp()
        {
            this.s1 = new InspectedState
                          {
                              State = "S1",
                              Description = "state 1"
                          };

            this.s2 = new InspectedState
                          {
                              State = "S2",
                              Description = "state 2"
                          };

            this.StateService.GetAll()
                .Returns(new
                    SuccessResult<IEnumerable<InspectedState>>(new List<InspectedState>
                                                                 {
                                                                     this.s1,
                                                                     this.s2
                                                                 }));

            this.Response = this.Browser.Get(
                "/inventory/stock-locators/states",
                with =>
                    {
                        with.Header("Accept", "application/json");
                    }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.StateService.Received().GetAll();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<IEnumerable<InspectedState>>().ToList();
            resultResource.Should().HaveCount(2);
        }
    }
}
