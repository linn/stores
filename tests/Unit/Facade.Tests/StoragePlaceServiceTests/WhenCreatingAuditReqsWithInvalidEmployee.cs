namespace Linn.Stores.Facade.Tests.StoragePlaceServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources.RequestResources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenCreatingAuditReqsWithInvalidEmployee : ContextBase
    {
        private CreateAuditReqsResource resource;

        [SetUp]
        public void SetUp()
        {
            this.resource = new CreateAuditReqsResource
                                {
                                    LocationRange = "E-K1-01",
                                    Department = "DEP",
                                    Links = new List<LinkResource>
                                                {
                                                    new LinkResource("created-by", string.Empty)
                                                }.ToArray()
                                };

            this.StoragePlaceRepository.FilterBy(Arg.Any<Expression<Func<StoragePlace, bool>>>()).Returns(
                new List<StoragePlace> { new StoragePlace { Name = "SP" } }.AsQueryable());

            this.StoragePlaceAuditPack.CreateAuditReq("SP", 33067, this.resource.Department).Returns("ERROR");
        }

        [Test]
        public void ShouldThrowException()
        {
            Assert.Throws<InvalidOperationException>(() => this.Sut.CreateAuditReqs(this.resource));
        }
    }
}