namespace Linn.Stores.Service.Tests.StockLocatorsModuleSpecs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Requisitions;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingMoves : ContextBase
    {
        private StockMove s1;

        private StockMove s2;

        [SetUp]
        public void SetUp()
        {
            this.s1 = new StockMove(new ReqMove
                                        {
                                            ReqNumber = 1,
                                            LineNumber = 1,
                                            Sequence = 1,
                                            Quantity = 1m
                                        });
                          

            this.s2 = new StockMove(new ReqMove
                                        {
                                            ReqNumber = 1,
                                            LineNumber = 2,
                                            Sequence = 1,
                                            Quantity = 1m
                                        });

            this.StockLocatorFacadeService.GetMoves(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int?>())
                .Returns(new
                    SuccessResult<IEnumerable<StockMove>>(new List<StockMove>
                                                              {
                                                                  this.s1,
                                                                  this.s2
                                                              }));

            this.Response = this.Browser.Get(
                "/inventory/stock-locators/stock-moves",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("searchTerm", "PART");
                        with.Query("palletNumber", "1");
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
            this.StockLocatorFacadeService.Received().GetMoves("PART", 1, null);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<IEnumerable<StockMove>>().ToList();
            resultResource.Should().HaveCount(2);
        }
    }
}
