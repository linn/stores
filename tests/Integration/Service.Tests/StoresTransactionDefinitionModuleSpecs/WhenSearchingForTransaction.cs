namespace Linn.Stores.Service.Tests.StoresTransactionDefinitionModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenSearchingForTransaction : ContextBase
    {
        private string searchTerm;

        [SetUp]
        public void SetUp()
        {
            this.searchTerm = "STSTM";

            this.StoresTransactionDefinitionFacadeService.Search(this.searchTerm).Returns(
                new SuccessResult<IEnumerable<StoresTransactionDefinition>>(
                    new List<StoresTransactionDefinition> { new StoresTransactionDefinition { TransactionCode = "STSTM1", Description = "Stores move 1" }, new StoresTransactionDefinition { TransactionCode = "STSTM2", Description = "Stores move 2" } }));

            this.Response = this.Browser.Get(
                "/inventory/stores-transaction-definitions",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("searchTerm",this.searchTerm);
                    }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resultResource = this.Response.Body.DeserializeJson<IEnumerable<StoresTransactionDefinitionResource>>().ToList();
            resultResource.Should().HaveCount(2);
            resultResource.FirstOrDefault(a => a.TransactionCode == "STSTM1").Should().NotBeNull();
            resultResource.FirstOrDefault(a => a.TransactionCode == "STSTM2").Should().NotBeNull();
        }
    }
}
