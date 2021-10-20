namespace Linn.Stores.Domain.LinnApps.Tests.ImportBookServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Castle.DynamicProxy.Generators.Emitters.SimpleAST;

    using FluentAssertions;

    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Domain.LinnApps.Models;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenPostingDuty : ContextBase
    {
        private readonly int impbookId = 12007;

        private ImportBookOrderDetail firstOrderDetail;
        
        private ProcessResult result;

        private ImportBookOrderDetail secondOrderDetail;

        [SetUp]
        public void SetUp()
        {
            this.firstOrderDetail = new ImportBookOrderDetail
                                        {
                                            ImportBookId = this.impbookId,
                                            LineNumber = 1,
                                            OrderNumber = null,
                                            RsnNumber = null,
                                            OrderDescription = "kylo ren first order",
                                            Qty = 1,
                                            DutyValue = 21.12m,
                                            FreightValue = 22.12m,
                                            VatValue = 3.12m,
                                            OrderValue = 44.1m,
                                            Weight = 55.2m,
                                            LoanNumber = null,
                                            LineType = "typea",
                                            CpcNumber = null,
                                            TariffCode = "121213",
                                            InsNumber = null,
                                            VatRate = null,
                                            PostDuty = null
                                        };

            this.secondOrderDetail = new ImportBookOrderDetail
                                         {
                                             ImportBookId = this.impbookId,
                                             LineNumber = 2,
                                             OrderNumber = 13,
                                             RsnNumber = 2,
                                             OrderDescription = "palpatine final order",
                                             Qty = 1,
                                             DutyValue = 21.12m,
                                             FreightValue = 22.12m,
                                             VatValue = 3.12m,
                                             OrderValue = 44.1m,
                                             Weight = 55.2m,
                                             LoanNumber = null,
                                             LineType = "TYpe B",
                                             CpcNumber = null,
                                             TariffCode = "121213",
                                             InsNumber = null,
                                             VatRate = null,
                                             PostDuty = null
            };
            
            this.SupplierRepository.FindBy(Arg.Any<Expression<Func<Supplier, bool>>>()).Returns(new Supplier { AccountingCompany = "LINN" });

            this.OrderDetailRepository.FindById(Arg.Any<ImportBookOrderDetailKey>())
                .Returns(this.firstOrderDetail, this.secondOrderDetail);

            this.PurchaseLedgerPack.GetNextLedgerSeq().Returns(53, 54);
            this.PurchaseLedgerPack.GetLedgerPeriod().Returns(17);
            this.PurchaseLedgerPack.GetNomacc(Arg.Any<string>(), Arg.Any<string>()).Returns(2233);

            var orderDetails = new List<ImportBookOrderDetail> { new ImportBookOrderDetail { PostDuty = "Y" }, new ImportBookOrderDetail { PostDuty = "Y" } };

            this.result = this.Sut.PostDutyForOrderDetails(orderDetails, 1234, 56789, DateTime.Now);
        }

        [Test]
        public void ShouldReturnSuccesfulProcessResult()
        {
            this.result.Success.Should().BeTrue();
        }

        [Test]
        public void ShouldHaveUpdatedPostDutyFlagOnBothOrderDetails()
        {
            this.firstOrderDetail.PostDuty.Should().Be("Y");
            this.secondOrderDetail.PostDuty.Should().Be("Y");
        }
    }
}
