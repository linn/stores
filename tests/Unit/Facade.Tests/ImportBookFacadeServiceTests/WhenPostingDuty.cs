namespace Linn.Stores.Facade.Tests.ImportBookFacadeServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Resources.ImportBooks;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenPostingDuty : ContextBase
    {
        private readonly int empNo = 33107;

        private readonly int impbookId = 54321;

        private readonly DateTime now = DateTime.Now;

        private readonly int supplierId = 55328;

        private PostDutyResource resource;

        private IResult<ProcessResult> result;

        [SetUp]
        public void SetUp()
        {
            this.resource = new PostDutyResource
                                {
                                    ImpbookId = this.impbookId,
                                    SupplierId = this.supplierId,
                                    CurrentUserNumber = this.empNo,
                                    DatePosted = this.now.ToString("O"),
                                    OrderDetails = new List<ImportBookOrderDetailResource>
                                                       {
                                                           new ImportBookOrderDetailResource
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
                                                                   PostDuty = true
                                                               },
                                                           new ImportBookOrderDetailResource
                                                               {
                                                                   ImportBookId = this.impbookId,
                                                                   LineNumber = 1,
                                                                   OrderNumber = 111,
                                                                   RsnNumber = 222,
                                                                   OrderDescription = "kylo ren first order",
                                                                   Qty = 3,
                                                                   DutyValue = 91.12m,
                                                                   FreightValue = 92.12m,
                                                                   VatValue = 93.12m,
                                                                   OrderValue = 944.1m,
                                                                   Weight = 955.2m,
                                                                   LoanNumber = 999,
                                                                   LineType = "Type C",
                                                                   CpcNumber = 91,
                                                                   TariffCode = "121213",
                                                                   InsNumber = 92,
                                                                   VatRate = 93,
                                                                   PostDuty = false
                                                               }
                                                       }
                                };

            this.DomainService.PostDutyForOrderDetails(
                Arg.Any<IEnumerable<ImportBookOrderDetail>>(),
                Arg.Any<int>(),
                Arg.Any<int>(),
                Arg.Any<DateTime>()).Returns(new ProcessResult(true, "wurkd"));

            this.result = this.Sut.PostDuty(this.resource);
        }

        [Test]
        public void ShouldCallDomainWithRightData()
        {
            this.DomainService.Received().PostDutyForOrderDetails(
                Arg.Is<IEnumerable<ImportBookOrderDetail>>(
                    x => x.Any(
                        y => y.PostDuty && y.ImportBookId == this.impbookId && y.LineNumber == 2
                             && y.OrderNumber == 13)),
                this.supplierId,
                this.empNo,
                this.now);

            this.DomainService.Received().PostDutyForOrderDetails(
                Arg.Is<IEnumerable<ImportBookOrderDetail>>(
                    x => x.Any(
                        y => !y.PostDuty && y.ImportBookId == this.impbookId && y.LineNumber == 1
                             && y.OrderNumber == 111)),
                this.supplierId,
                this.empNo,
                this.now);
        }

        [Test]
        public void ShouldCallReturnSuccessResult()
        {
            this.result.Should().BeOfType<SuccessResult<ProcessResult>>();
        }
    }
}
