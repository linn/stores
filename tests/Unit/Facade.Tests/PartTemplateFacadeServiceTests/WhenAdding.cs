namespace Linn.Stores.Facade.Tests.PartTemplateFacadeServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenAdding : ContextBase
    {
        private PartTemplateResource resource;

        private IResult<PartTemplate> result;

        [SetUp]
        public void SetUp()
        {
            this.resource = new PartTemplateResource
            {
                PartRoot = "LRPT",
                Description = "A test for adding a part template",
                HasDataSheet = "N",
                HasNumberSequence = "Y",
                NextNumber = 32,
                AllowVariants = "Y",
                Variants = "a part template being added as part of a test.",
                AccountingCompany = "LINN RECORDS",
                ProductCode = "LINNPT",
                StockControlled = "Y",
                LinnProduced = "Y",
                RmFg = "L",
                BomType = "A",
                AssemblyTechnology = "RS",
                AllowPartCreation = "Y",
                ParetoCode = "J"
            };

            this.result = this.Sut.Add(this.resource);
        }

        [Test]
        public void ShouldAdd()
        {
            this.PartTemplateRepository.Received().Add(Arg.Any<PartTemplate>());
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<CreatedResult<PartTemplate>>();
            var dataResult = ((CreatedResult<PartTemplate>)this.result).Data;
            dataResult.PartRoot.Should().Be("LRPT");
            dataResult.Description.Should().Be("A test for adding a part template");
            dataResult.HasDataSheet.Should().Be("N");
            dataResult.HasNumberSequence.Should().Be("Y");
            dataResult.NextNumber.Should().Be(32);
            dataResult.AllowVariants.Should().Be("Y");
            dataResult.Variants.Should().Be("a part template being added as part of a test.");
            dataResult.AccountingCompany.Should().Be("LINN RECORDS");
            dataResult.ProductCode.Should().Be("LINNPT");
            dataResult.StockControlled.Should().Be("Y");
            dataResult.LinnProduced.Should().Be("Y");
            dataResult.RmFg.Should().Be("L");
            dataResult.BomType.Should().Be("A");
            dataResult.AssemblyTechnology.Should().Be("RS");
            dataResult.AllowPartCreation.Should().Be("Y");
            dataResult.ParetoCode.Should().Be("J");
        }
    }
}
