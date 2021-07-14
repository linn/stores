namespace Linn.Stores.Facade.Tests.PortFacadeServiceTests
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
        private IResult<IEnumerable<Port>> results;

        [SetUp]
        public void SetUp()
        {
            var ports = new List<Port>
                           {
                               new Port { PortCode = "AA", Description = "num1" },
                               new Port { PortCode = "BB", Description = "num2" }
                           }.AsQueryable();

            this.PortRepository.FindAll().Returns(ports);

            this.results = this.Sut.GetAll();
        }

        [Test]
        public void ShouldSearch()
        {
            this.PortRepository.Received().FindAll();
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.results.Should().BeOfType<SuccessResult<IEnumerable<Port>>>();
            var dataResult = ((SuccessResult<IEnumerable<Port>>)this.results).Data.ToList();
            dataResult.FirstOrDefault(x => x.PortCode == "AA" && x.Description == "num1").Should().NotBeNull();
            dataResult.FirstOrDefault(x => x.PortCode == "BB" && x.Description == "num2").Should().NotBeNull();
            dataResult.Count.Should().Be(2);
        }
    }
}
