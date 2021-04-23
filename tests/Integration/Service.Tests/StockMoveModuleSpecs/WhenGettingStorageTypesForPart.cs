namespace Linn.Stores.Service.Tests.StockMoveModuleSpecs
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

    public class WhenGettingStorageTypesForPart : ContextBase
    {
        private string search;

        private List<PartStorageType> storageTypes;
        [SetUp]
        public void SetUp()
        {
            this.search = "pn";
            this.storageTypes = new List<PartStorageType>
                             {
                                 new PartStorageType { StorageType = "1" }, 
                                 new PartStorageType { StorageType = "2" }
                             };
            this.PartStorageTypeFacadeService.Search(this.search)
                .Returns(new SuccessResult<IEnumerable<PartStorageType>>(this.storageTypes.AsQueryable()));

            this.Response = this.Browser.Get(
                "/inventory/part-storage-types",
                with =>
                {
                    with.Header("Accept", "application/json");
                    with.Query("searchTerm", this.search);
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
            this.PartStorageTypeFacadeService.Received().Search(this.search);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resources = this.Response.Body.DeserializeJson<IEnumerable<PartStorageTypeResource>>().ToList();
            resources.Should().HaveCount(2);
            resources.Should().Contain(a => a.StorageType == "1");
            resources.Should().Contain(a => a.StorageType == "2");
        }
    }
}
