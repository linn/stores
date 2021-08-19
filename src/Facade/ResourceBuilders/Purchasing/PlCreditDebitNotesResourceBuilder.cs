namespace Linn.Stores.Facade.ResourceBuilders.Purchasing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Purchasing;
    using Linn.Stores.Resources.Purchasing;

    public class PlCreditDebitNotesResourceBuilder : IResourceBuilder<IEnumerable<PlCreditDebitNote>>
    {
        private readonly PlCreditDebitNoteResourceBuilder resourceBuilder = new PlCreditDebitNoteResourceBuilder();

        public IEnumerable<PlCreditDebitNoteResource> Build(IEnumerable<PlCreditDebitNote> notes)
        {
            return notes
                .Select(a => this.resourceBuilder.Build(a));
        }

        object IResourceBuilder<IEnumerable<PlCreditDebitNote>>.Build(IEnumerable<PlCreditDebitNote> notes) => this.Build(notes);

        public string GetLocation(IEnumerable<PlCreditDebitNote> notes)
        {
            throw new NotImplementedException();
        }
    }
}
