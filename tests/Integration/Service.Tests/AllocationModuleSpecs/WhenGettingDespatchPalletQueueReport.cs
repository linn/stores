namespace Linn.Stores.Service.Tests.AllocationModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Allocation.Models;
    using Linn.Stores.Resources.Allocation;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingDespatchPalletQueueReport : ContextBase
    {
        private DespatchPalletQueueResult result;

        private DespatchPalletQueueResultDetail detail;

        [SetUp]
        public void SetUp()
        {
            this.detail = new DespatchPalletQueueResultDetail
                              {
                                  KittedFromTime = "e",
                                  PalletNumber = 234,
                                  PickingSequence = 54,
                                  WarehouseInformation = "at SA"
                              };
            this.result = new DespatchPalletQueueResult
                              {
                                  TotalNumberOfPallets = 10,
                                  NumberOfPalletsToMove = 8,
                                  DespatchPalletQueueResultDetails = new List<DespatchPalletQueueResultDetail>
                                                                         {
                                                                             this.detail
                                                                         }
                              };
            this.AllocationFacadeService.DespatchPalletQueueReport()
                .Returns(new SuccessResult<DespatchPalletQueueResult>(this.result));

            this.Response = this.Browser.Get(
                "/logistics/allocations/despatch-pallet-queue",
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
            this.AllocationFacadeService.Received().DespatchPalletQueueReport();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<DespatchPalletQueueResultResource>();
            resultResource.NumberOfPalletsToMove.Should().Be(this.result.NumberOfPalletsToMove);
            resultResource.TotalNumberOfPallets.Should().Be(this.result.TotalNumberOfPallets);
            resultResource.DespatchPalletQueueDetails.Should().HaveCount(1);
            resultResource.DespatchPalletQueueDetails.First().KittedFromTime.Should().Be(this.detail.KittedFromTime);
            resultResource.DespatchPalletQueueDetails.First().PalletNumber.Should().Be(this.detail.PalletNumber);
            resultResource.DespatchPalletQueueDetails.First().WarehouseInformation.Should().Be(this.detail.WarehouseInformation);
            resultResource.DespatchPalletQueueDetails.First().PickingSequence.Should().Be(this.detail.PickingSequence);
        }
    }
}
