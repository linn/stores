namespace Linn.Stores.Service.Tests.AllocationModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Allocation;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.Allocation;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingSosAllocDetails : ContextBase
    {
        private SosAllocDetail sosAllocDetail1;

        private SosAllocDetail sosAllocDetail2;

        private JobIdRequestResource searchRequestResource;

        [SetUp]
        public void SetUp()
        {
            this.searchRequestResource = new JobIdRequestResource { JobId = 808 };
            this.sosAllocDetail1 = new SosAllocDetail { Id = 1, AccountId = 1, JobId = 808 };
            this.sosAllocDetail2 = new SosAllocDetail { Id = 2, AccountId = 2, JobId = 808 };

            this.SosAllocDetailFacadeService
                .FilterBy(Arg.Is<JobIdRequestResource>(a => a.JobId == this.searchRequestResource.JobId))
                .Returns(new SuccessResult<IEnumerable<SosAllocDetail>>(new List<SosAllocDetail>
                                                                            {
                                                                                this.sosAllocDetail1, this.sosAllocDetail2
                                                                            }));

            this.Response = this.Browser.Get(
                "/logistics/sos-alloc-details",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.JsonBody(this.searchRequestResource);
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
            this.SosAllocDetailFacadeService
                .Received()
                .FilterBy(Arg.Is<JobIdRequestResource>(a => a.JobId == this.searchRequestResource.JobId));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<IEnumerable<SosAllocDetailResource>>().ToList();
            resultResource.Should().HaveCount(2);
            resultResource.Should().Contain(a => a.Id == 1);
            resultResource.Should().Contain(a => a.Id == 2);
        }
    }
}
