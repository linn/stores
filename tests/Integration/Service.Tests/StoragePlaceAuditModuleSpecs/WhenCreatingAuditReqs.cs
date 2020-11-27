namespace Linn.Stores.Service.Tests.StoragePlaceAuditModuleSpecs
{
    using Linn.Stores.Resources.RequestResources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenCreatingAuditReqs : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Response = this.Browser.Post(
                "/inventory/storage-places/create-audit-reqs",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("locationRange", "loc");
                    }).Result;
        }

        [Test]
        public void ShouldCallService()
        {
            this.StoragePlaceService.Received().CreateAuditReqs(Arg.Any<CreateAuditReqsResource>());
        }
    }
}