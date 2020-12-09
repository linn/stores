namespace Linn.Stores.Facade.Tests.MechPartSourceFacadeServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdating : ContextBase
    {
        private MechPartSourceResource resource;

        private IResult<MechPartSource> result;

        [SetUp]
        public void SetUp()
        {
            var employee = new Employee { Id = 33870 };
            var dataSheets = new List<PartDataSheet>
                                 {
                                     new PartDataSheet
                                         {
                                             Sequence = 1,
                                             PdfFilePath = "/path/1"
                                         },
                                 };
            var part = new Part { Id = 12, PartNumber = "PART 012", DataSheets = dataSheets, };
                
            var mechPartSource = new MechPartSource
                                     {
                                         Id = 1,
                                         DateSamplesRequired = DateTime.Today,
                                         ProposedBy = employee,
                                         ProductionDate = DateTime.Today,
                                         LinnPartNumber = "PART 123",
                                         Part = part,
                                     };

            var dataSheetsResource = new List<PartDataSheetResource>
                                 {
                                     new PartDataSheetResource
                                         {
                                            Sequence = 1,
                                            PdfFilePath = "/path/1"
                                         },
                                     new PartDataSheetResource
                                         {
                                             Sequence = 2,
                                             PdfFilePath = "/path/2"
                                         },
                                 };

            var mechPartAltsResource = new List<MechPartAltResource>
                                           {
                                               new MechPartAltResource
                                                   {
                                                       PartNumber = "PART 012", 
                                                       Sequence = 1, 
                                                       SupplierId = 1, 
                                                       SupplierName = "SUP"
                                                   }
                                           };
            
            var mechPartManufacturerAlts = new List<MechPartManufacturerAltResource>
                                               {
                                                   new MechPartManufacturerAltResource
                                                       {
                                                           PartNumber = "PART 012", 
                                                           Sequence = 1, 
                                                           ManufacturerCode = "M"
                                                       }
                                               };
            
            this.resource = new MechPartSourceResource
                                {
                                    Id = 1,
                                    DateSamplesRequired = DateTime.Today.ToString("o"),
                                    ProposedBy = 33870,
                                    ProductionDate = DateTime.Today.ToString("o"),
                                    LinnPartNumber = "PART 123",
                                    MechPartAlts = mechPartAltsResource,
                                    Part = new PartResource
                                               {
                                                   PartNumber = "PART 012",
                                                   DataSheets = dataSheetsResource,
                                               },
                                    MechPartManufacturerAlts = mechPartManufacturerAlts
                                };
            this.PartRepository.FindById(12).Returns(part);
            this.EmployeeRepository.FindById(33870).Returns(employee);
            this.SupplierRepository.FindBy(Arg.Any<Expression<Func<Supplier, bool>>>())
                .Returns(new Supplier { Id = 1 });
            this.MechPartSourceRepository.FindById(1).Returns(mechPartSource);
            this.result = this.Sut.Update(this.resource.Id, this.resource);
        }

        [Test]
        public void ShouldGet()
        {
            this.MechPartSourceRepository.Received().FindById(1);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<MechPartSource>>();
            var dataResult = ((SuccessResult<MechPartSource>)this.result).Data;
            dataResult.Part.Id.Should().Be(12);
        }
    }
}
