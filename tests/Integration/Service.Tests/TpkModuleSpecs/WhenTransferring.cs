namespace Linn.Stores.Service.Tests.TpkModuleSpecs
{
    using System;
    using System.Collections.Generic;
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Tpk;
    using Linn.Stores.Domain.LinnApps.Tpk.Models;
    using Linn.Stores.Resources.Tpk;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenTransferring : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var tpkRequest = new TpkRequestResource
                                 {
                                     DateTimeTpkViewQueried = DateTime.UnixEpoch,
                                     StockToTransfer = new List<TransferableStockResource>
                                                           {
                                                               new TransferableStockResource
                                                                   {
                                                                       FromLocation = "From",
                                                                       ConsignmentId = 1,
                                                                       Addressee = "address",
                                                                       ArticleNumber = "Article",
                                                                       DespatchLocationCode = "Code",
                                                                       LocationId = 1,
                                                                       VaxPallet = 1
                                                                   }
                                                           }
                                 };
           

            this.TpkFacadeService.TransferStock(Arg.Any<TpkRequestResource>())
                .Returns(new SuccessResult<TpkResult>(new TpkResult
                                                          {
                                                              Transferred = new List<TransferredStock>
                                                                                {
                                                                                    new TransferredStock(
                                                                                        new TransferableStock 
                                                                                            {
                                                                                                ConsignmentId = 1,
                                                                                                LocationId = 1,
                                                                                                PalletNumber = 1
                                                                                            }, 
                                                                                        "note")
                                                                                },
                                                              Message = "Msg",
                                                              Success = true
                                                          }));


            this.Response = this.Browser.Post(
                "/logistics/tpk/transfer",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.JsonBody(tpkRequest);
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
            this.TpkFacadeService.Received().TransferStock(Arg.Any<TpkRequestResource>());
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<TpkResult>();
            resource.Success.Should().BeTrue();
        }
    }
}
