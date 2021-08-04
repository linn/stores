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
                           OrderQty = note.OrderQty,
                           PartNumber = note.PartNumber,
                           DateClosed = note.DateClosed?.ToString("o"),
                           SupplierId = note.SupplierId,
                           ClosedBy = note.ClosedBy,
                           NetTotal = note.NetTotal,
                           NoteNumber = note.NoteNumber,
                           OriginalOrderNumber = note.OriginalOrderNumber,
                           ReturnsOrderNumber = note.ReturnsOrderNumber,
                           Notes = note.Notes,
                           SupplierName = note.Supplier?.Name
                       };
        }

        public string GetLocation(PlCreditDebitNote note)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<PlCreditDebitNote>.Build(PlCreditDebitNote note) => this.Build(note);
    }
}
