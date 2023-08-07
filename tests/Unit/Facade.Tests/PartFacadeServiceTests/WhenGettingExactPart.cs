namespace Linn.Stores.Facade.Tests.PartFacadeServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingExactPart : ContextBase
    {
        private IResult<IEnumerable<Part>> results;
        private string partNumber;

        [SetUp]
        public void SetUp()
        {
            this.partNumber = "P1";
            var part = new Part { Id = 1, PartNumber = this.partNumber };

            this.PartRepository.FindBy(Arg.Any<Expression<Func<Part, bool>>>())
                .Returns(part);

            this.results = this.Sut.GetPartByPartNumber(this.partNumber);
        }

        [Test]
        public void ShouldSearch()
        {
            this.PartRepository.Received().FindBy(Arg.Any<Expression<Func<Part, bool>>>());
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.results.Should().BeOfType<SuccessResult<IEnumerable<Part>>>();
            var dataResult = ((SuccessResult<IEnumerable<Part>>)this.results).Data.ToList();
            dataResult.First(x => x.PartNumber == "P1").Id.Should().Be(1);
            dataResult.Count.Should().Be(1);
        }
    }
}
