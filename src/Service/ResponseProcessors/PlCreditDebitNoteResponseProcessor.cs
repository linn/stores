namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Purchasing;

    public class PlCreditDebitNoteResponseProcessor : JsonResponseProcessor<PlCreditDebitNote>
    {
        public PlCreditDebitNoteResponseProcessor(IResourceBuilder<PlCreditDebitNote> resourceBuilder)
            : base(resourceBuilder, "debit-note", 1)
        {
        }
    }
}
