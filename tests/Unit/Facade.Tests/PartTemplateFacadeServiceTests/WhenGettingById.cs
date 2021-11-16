namespace Linn.Stores.Facade.Tests.PartTemplateFacadeServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingById : ContextBase
    {
        private readonly string partTempId = "LRPT";

        private IResult<PartTemplate> result;

        [SetUp]
        public void SetUp()
        {
            var partTemplate = new PartTemplate
                                   {
                                       PartRoot = this.partTempId,
                                       Description = "A test for getting a part template by id",
                                       HasDataSheet = "N",
                                       HasNumberSequence = "Y",
                                       NextNumber = 32,
                                       AllowVariants = "Y",
                                       Variants = "the id is retrieving the part template",
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

            this.PartTemplateRepository.FindById(Arg.Any<string>()).Returns(partTemplate);

            this.result = this.Sut.GetById(this.partTempId);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<PartTemplate>>();
            var dataResult = ((SuccessResult<PartTemplate>)this.result).Data;
            dataResult.PartRoot.Should().Be(this.partTempId);
            dataResult.Description.Should().Be("A test for getting a part template by id");
            dataResult.HasDataSheet.Should().Be("N");
            dataResult.HasNumberSequence.Should().Be("Y");
            dataResult.NextNumber.Should().Be(32);
            dataResult.AllowVariants.Should().Be("Y");
            dataResult.Variants.Should().Be("the id is retrieving the part template");
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
