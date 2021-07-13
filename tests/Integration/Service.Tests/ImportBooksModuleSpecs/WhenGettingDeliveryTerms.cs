namespace Linn.Stores.Service.Tests.ImportBooksModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.ImportBooks;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingDeliveryTerms : ContextBase
    {
        private IList<ImportBookDeliveryTerm> deliveryTerms = new List<ImportBookDeliveryTerm>
                                                                  {
                                                                      new ImportBookDeliveryTerm
                                                                          {
                                                                              DeliveryTermCode = "abc",
                                                                              Description = "a ship",
                                                                              Comments = "none"
                                                                          },
                                                                      new ImportBookDeliveryTerm
                                                                          {
                                                                              DeliveryTermCode = "def",
                                                                              Description = "something else",
                                                                              Comments = "maybe a ship"
                                                                          },
                                                                  };

        [SetUp]
        public void SetUp()
        {
            this.ImportBookDeliveryTermFacadeService.GetAll().Returns(
                new SuccessResult<IEnumerable<ImportBookDeliveryTerm>>(deliveryTerms));

            this.Response = this.Browser.Get(
                "/logistics/import-books/delivery-terms",
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
            this.ImportBookDeliveryTermFacadeService.Received().GetAll();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<ImportBookDeliveryTermResource>>();
            resource.Count().Should().Be(2);
            resource.Any(
                x => x.DeliveryTermCode == this.deliveryTerms[0].DeliveryTermCode
                     && x.Description == this.deliveryTerms[0].Description
                     && x.Comments == this.deliveryTerms[0].Comments).Should().BeTrue();
            resource.Any(
                x => x.DeliveryTermCode == this.deliveryTerms[1].DeliveryTermCode
                     && x.Description == this.deliveryTerms[1].Description
                     && x.Comments == this.deliveryTerms[1].Comments).Should().BeTrue();
        }
    }
}
