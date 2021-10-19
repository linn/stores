namespace Linn.Stores.Domain.LinnApps.Tests.StockLocatorServiceTests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Domain.LinnApps.StockLocators;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingStockRotationsAndTooManyParts : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var list = new List<StockLocator>();

            for (var i = 0; i < 101; i++)
            {
                list.Add(new StockLocator { Quantity = 1, PartNumber = i.ToString() });
            }

            this.StockLocatorRepository.FilterByWildcard("RES%")
                .Returns(list.AsQueryable());
        }

        [Test]
        public void ShouldThrowException()
        {
            var ex = Assert.Throws<StockLocatorException>(()
                    => this.Sut.GetBatchesInRotationOrderByPart("RES*"));

            ex.Message.Should().Be("Too many results for the report to handle. Please refine your Part Number search");
        }
    }
}
