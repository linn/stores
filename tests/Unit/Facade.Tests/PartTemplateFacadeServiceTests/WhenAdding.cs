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
            dataResult.PartRoot.Should().ContainAll("LRPT");
            dataResult.Description.Should().ContainAll("A test for adding a part template");
            dataResult.HasDataSheet.Should().ContainAll("N");
            dataResult.HasNumberSequence.Should().ContainAll("Y");
            dataResult.NextNumber.Should().Be(32);
            dataResult.AllowVariants.Should().ContainAll("Y");
            dataResult.Variants.Should().ContainAll("a part template being added as part of a test.");
            dataResult.AccountingCompany.Should().ContainAll("LINN RECORDS");
            dataResult.ProductCode.Should().ContainAll("LINNPT");
            dataResult.StockControlled.Should().ContainAll("Y");
            dataResult.LinnProduced.Should().ContainAll("Y");
            dataResult.RmFg.Should().ContainAll("L");
            dataResult.BomType.Should().ContainAll("A");
            dataResult.AssemblyTechnology.Should().ContainAll("RS");
            dataResult.AllowPartCreation.Should().ContainAll("Y");
            dataResult.ParetoCode.Should().ContainAll("J");
        }
    }
}
