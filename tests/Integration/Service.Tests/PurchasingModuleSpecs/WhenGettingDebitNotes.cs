namespace Linn.Stores.Service.Tests.PurchasingModuleSpecs
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Purchasing;
    using Linn.Stores.Resources.Purchasing;

    using Nancy;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingDebitNotes : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var debitNote1 = new PlCreditDebitNote { NoteNumber = 1, OrderQty = 1, NetTotal = 10m };
            var debitNote2 = new PlCreditDebitNote { NoteNumber = 2, OrderQty = 1, NetTotal = 10m };

            this.DebitNoteService.FilterBy(Arg.Any<PlCreditDebitNoteResource>()).Returns(
                new SuccessResult<IEnumerable<PlCreditDebitNote>>(
                    new List<PlCreditDebitNote> { debitNote1, debitNote2 }));

            this.Response = this
                .Browser
                .Get(
                    "/inventory/purchasing/debit-notes",
                    with =>
                        {
                            with.Header("Accept", "application/json");
                        }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.DebitNoteService.Received().FilterBy(Arg.Any<PlCreditDebitNoteResource>());
        }
    }
}
