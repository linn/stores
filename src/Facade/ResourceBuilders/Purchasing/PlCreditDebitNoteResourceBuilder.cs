namespace Linn.Stores.Facade.ResourceBuilders.Purchasing
{
    using System;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Purchasing;
    using Linn.Stores.Resources.Purchasing;

    public class PlCreditDebitNoteResourceBuilder : IResourceBuilder<PlCreditDebitNote>
    {
        public PlCreditDebitNoteResource Build(PlCreditDebitNote note)
        {
            return new PlCreditDebitNoteResource
                       {
                       };
        }

        public string GetLocation(PlCreditDebitNote note)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<PlCreditDebitNote>.Build(PlCreditDebitNote note) => this.Build(note);
    }
}
