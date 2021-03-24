namespace Linn.Stores.Service.Tests.RequisitionModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Requisitions;
    using Linn.Stores.Resources.Requisitions;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingRequisitionMoves : ContextBase
    {
        private RequisitionHeader requisition;

        [SetUp]
        public void SetUp()
        {
            this.requisition = new RequisitionHeader
                                   {
                                       ReqNumber = 3243,
                                       Lines = new List<RequisitionLine>
                                                   {
                                                       new RequisitionLine
                                                           {
                                                               LineNumber = 1,
                                                               PartNumber = "part1",
                                                               Moves = new List<ReqMove>
                                                                           {
                                                                               new ReqMove { Sequence = 1, Quantity = 3 }
                                                                           }
                                                           },
                                                       new RequisitionLine
                                                           {
                                                               LineNumber = 2,
                                                               PartNumber = "part2",
                                                               Moves = new List<ReqMove>
                                                                           {
                                                                               new ReqMove { Sequence = 1, Quantity = 6 },
                                                                               new ReqMove { Sequence = 2, Quantity = 1 }
                                                                           }
                                                           }
                                                   }
                                   };
            this.RequisitionFacadeService.GetById(this.requisition.ReqNumber).Returns(new SuccessResult<RequisitionHeader>(this.requisition));
            this.Response = this.Browser.Get(
                $"/logistics/requisitions/{this.requisition.ReqNumber}",
                with =>
                {
                    with.Header("Accept", "application/vnd.linn.req-moves-summary+json;version=1");
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
            this.RequisitionFacadeService.Received().GetById(this.requisition.ReqNumber);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<RequisitionMoveSummaryResource>>().ToList();
            resource.Should().HaveCount(3);
            resource.First(a => a.LineNumber == 1).PartNumber.Should().Be("part1");
            resource.First(a => a.LineNumber == 1).MoveSeq.Should().Be(1);
            resource.First(a => a.LineNumber == 1).MoveQuantity.Should().Be(3);
        }
    }
}
