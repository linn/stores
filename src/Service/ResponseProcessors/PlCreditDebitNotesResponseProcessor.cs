namespace Linn.Stores.Service.ResponseProcessors
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.Purchasing;

    public class PlCreditDebitNotesResponseProcessor : JsonResponseProcessor<IEnumerable<PlCreditDebitNote>>
    {
        public PlCreditDebitNotesResponseProcessor(IResourceBuilder<IEnumerable<PlCreditDebitNote>> resourceBuilder)
            : base(resourceBuilder, "pl-credit-debit-notes", 1)
        {
        }
    }
}
