namespace Linn.Stores.Domain.LinnApps.Tests.GoodsInServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Domain.LinnApps.Parts;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenPrintingLabelsAndQcQuarantined : ContextBase
    {
        private readonly string dateString = DateTime.Today.ToString("MMMddyyyy").ToUpper();

        private ProcessResult result;

        [SetUp]
        public void SetUp()
        {
            this.LabelTypeRepository.FindBy(Arg.Any<Expression<Func<StoresLabelType, bool>>>())
                .Returns(new StoresLabelType
                {
                    Code = "QUARANTINE",
                    FileName = "template.ext",
                    DefaultPrinter = "Printer"
                });
            this.AuthUserRepository.FindBy(Arg.Any<Expression<Func<AuthUser, bool>>>())
                .Returns(new AuthUser
                {
                    Initials = "SU",
                    Name = "Some User",
                    UserNumber = 1
                });
            this.PurchaseOrderRepository.FindById(1).Returns(new PurchaseOrder
            {
                OrderNumber = 1,
                Supplier = new Supplier
                               {
                                   Id = 1,
                                   Name = "SUPPLIER"
                               },
                Details = new List<PurchaseOrderDetail>
                              {
                                  new PurchaseOrderDetail
                                      {
                                          RohsCompliant = "Y"
                                      }
                              }
            });
            this.PartsRepository.FindBy(Arg.Any<Expression<Func<Part, bool>>>())
                .Returns(new Part
                {
                    PartNumber = "PART",
                    Description = "DESCRIPTION"
                });

            this.Bartender.PrintLabels(
                "QC 1",
                "Printer",
                1,
                "template.ext",
                $"\"PO1\",\"PART\",\"DESCRIPTION\",\"DELIVERY-REF\",\"{this.dateString}\",\"\",\"SU\",\"{this.dateString}\",\"NO QC INFO\",\"0\",\"SUPPLIER\",\"1\",\"1\",\"1\",\"1\",\"QUARANTINE\",\"DATE TESTED\",\"1\"{Environment.NewLine}",
                ref Arg.Any<string>()).Returns(true);

            this.result = this.Sut.PrintLabels(
                "PO",
                "PART",
                "DELIVERY-REF",
                1,
                1,
                1,
                1,
                "QUARANTINE",
                1,
                null);
        }

        [Test]
        public void ShouldCallBartenderWithCorrectParameters()
        {
            this.Bartender.Received(1).PrintLabels(
                "QC 1",
                "Printer",
                1,
                "template.ext",
                $"\"PO1\",\"PART\",\"DESCRIPTION\",\"DELIVERY-REF\",\"{this.dateString}\",\"\",\"SU\",\"{this.dateString}\",\"NO QC INFO\",\"0\",\"SUPPLIER\",\"1\",\"1\",\"1\",\"1\",\"QUARANTINE\",\"DATE TESTED\",\"1\"{Environment.NewLine}",
                ref Arg.Any<string>());
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Success.Should().BeTrue();
        }
    }
}
