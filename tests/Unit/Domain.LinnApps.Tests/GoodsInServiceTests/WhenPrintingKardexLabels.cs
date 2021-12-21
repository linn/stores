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

    public class WhenPrintingKardexLabels : ContextBase
    {
        private ProcessResult result;

        private readonly string dateString = DateTime.Today.ToString("MMMddyyyy").ToUpper();

        [SetUp]
        public void SetUp()
        {
            this.LabelTypeRepository.FindBy(Arg.Any<Expression<Func<StoresLabelType, bool>>>())
                .Returns(
                    new StoresLabelType
                    {
                        Code = "KARDEX",
                        FileName = "kardex-template.ext",
                        DefaultPrinter = "Printer"
                    }, 
                    new StoresLabelType
                       {
                           Code = "PASS",
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
            this.PurchaseOrderRepository.FindById(1)
                .Returns(new PurchaseOrder
                {
                    OrderNumber = 1,
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
                    PartNumber = "PART"
                });

            this.Bartender.PrintLabels(
                "KGI1",
                "Printer",
                1,
                "kardex-template.ext",
                $"\"kardex-location\",\"1\"",
                ref Arg.Any<string>()).Returns(true);

            this.Bartender.PrintLabels(
                Arg.Any<string>(),
                "Printer",
                Arg.Any<int>(),
                "template.ext",
                Arg.Any<string>(),
                ref Arg.Any<string>()).Returns(true);

            this.result = this.Sut.PrintLabels(
                "PO",
                "PART",
                "DELIVERY-REF",
                1,
                1,
                1,
                1,
                "PASS",
                1,
                "kardex-location",
                new List<GoodsInLabelLine> { new GoodsInLabelLine { Id = 1, Qty = 1m } });
        }

        [Test]
        public void ShouldPrintBigLabels()
        {
            this.Bartender.Received(1).PrintLabels(
                "QC 1-1",
                "Printer",
                1,
                "template.ext",
                $"\"1\",\"PART\",\"1\",\"SU\",\"\",\"1\",\"{this.dateString}\",\"**ROHS Compliant**\"{Environment.NewLine}",
                ref Arg.Any<string>());
        }

        [Test]
        public void ShouldPrintOneMoreKardexLabelThanBigLabels()
        {
            this.Bartender.Received().PrintLabels(
                "KGI1",
                "Printer",
                2,
                "kardex-template.ext",
                $"\"kardex-location\",\"1\"",
                ref Arg.Any<string>());
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Success.Should().BeTrue();
        }
    }
}
