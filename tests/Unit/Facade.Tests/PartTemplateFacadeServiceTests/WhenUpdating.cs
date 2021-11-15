namespace Linn.Stores.Facade.Tests.PartTemplateFacadeServiceTests
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdating : ContextBase
    {
        private readonly string partTempId = "LRPT";

        private PartTemplateResource resource;

        private IResult<PartTemplate> result;

        [SetUp]
        public void SetUp()
        {
            var partTemplate = new PartTemplate
                                   {
                                       PartRoot = "LRPT",
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

            this.resource = new PartTemplateResource
                                {
                                    PartRoot = "LRPT",
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

            this.result = this.Sut.Add(this.resource);

            this.PartTemplateRepository.FindById(this.partTempId).Returns(partTemplate);

            this.result = this.Sut.Update(this.resource.PartRoot, this.resource);
        }

        [Test]
        public void ShouldGet()
        {
            this.PartTemplateRepository.Received().FindById(this.partTempId);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<PartTemplate>>();
            var dataResult = ((SuccessResult<PartTemplate>)this.result).Data;
            dataResult.PartRoot.Should().ContainAll(this.partTempId);
            dataResult.Description.Should().ContainAll("A test for getting a part template by id");
            dataResult.HasDataSheet.Should().ContainAll("N");
            dataResult.HasNumberSequence.Should().ContainAll("Y");
            dataResult.NextNumber.Should().Be(32);
            dataResult.AllowVariants.Should().ContainAll("Y");
            dataResult.Variants.Should().ContainAll("the id is retrieving the part template");
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
