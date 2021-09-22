namespace Linn.Stores.Service.Tests.PurchasingModuleSpecs
{
    using FluentAssertions;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Purchasing;
    using Linn.Stores.Resources.Purchasing;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenUpdatingDebitNote : ContextBase
    {
        private PlCreditDebitNoteResource resource;

        [SetUp]
        public void SetUp()
        {
            this.resource = new PlCreditDebitNoteResource
                                {
                                    NoteNumber = 1, ClosedBy = 33087, NetTotal = 1m, OrderQty = 1
                                };
            var updated = new PlCreditDebitNote
                              {
                                  NoteNumber = 1, ClosedBy = 33087, NetTotal = 1m, OrderQty = 1
                              };

            this.DebitNoteService.Update(1, Arg.Any<PlCreditDebitNoteResource>())
                .Returns(new SuccessResult<PlCreditDebitNote>(updated));

            this.Response = this.Browser.Put(
                "/inventory/purchasing/debit-notes/1",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Header("Content-Type", "application/json");
                        with.JsonBody(this.resource);
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
            this.DebitNoteService
                .Received()
                .Update(1, Arg.Is<PlCreditDebitNoteResource>(r => r.NoteNumber == this.resource.NoteNumber));
        }
    }
}
