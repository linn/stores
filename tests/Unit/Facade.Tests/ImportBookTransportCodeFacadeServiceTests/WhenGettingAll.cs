namespace Linn.Stores.Facade.Tests.ImportBookTransportCodeFacadeServiceTests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenSearching : ContextBase
    {
        private IResult<IEnumerable<ImportBookTransportCode>> results;

        [SetUp]
        public void SetUp()
        {
            var transportCode = new List<ImportBookTransportCode>
                                              {
                                                  new ImportBookTransportCode { TransportId = 1, Description = "Boat" },
                                                  new ImportBookTransportCode { TransportId = 2, Description = "Air" }
                                              }.AsQueryable();

            this.ImportBookTransportCodeRepository.FindAll().Returns(transportCode);

            this.results = this.Sut.GetAll();
        }

        [Test]
        public void ShouldSearch()
        {
            this.ImportBookTransportCodeRepository.Received().FindAll();
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.results.Should().BeOfType<SuccessResult<IEnumerable<ImportBookTransportCode>>>();
            var dataResult = ((SuccessResult<IEnumerable<ImportBookTransportCode>>)this.results).Data.ToList();
            dataResult.FirstOrDefault(x => x.TransportId == 1).Should().NotBeNull();
            dataResult.FirstOrDefault(x => x.TransportId == 2).Should().NotBeNull();
            dataResult.Count.Should().Be(2);
        }
    }
}
