namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.Parts;

    public class ImportBookPostEntryResourceBuilder : IResourceBuilder<ImpBookPostEntry>
    {
        public ImportBookPostEntryResource Build(ImpBookPostEntry model)
        {
            return new ImportBookPostEntryResource
                       {
                           ImportBookId = model.ImpBookId,
                           LineNumber = model.LineNumber,
                           EntryCodePrefix = model.EntryCodePrefix,
                           EntryCode = model.EntryCode,
                           EntryDate = model.EntryDate?.ToString("o"),
                           Reference = model.Reference,
                           Duty = model.Duty,
                           Vat = model.Vat
                       };
        }

        object IResourceBuilder<ImpBookPostEntry>.Build(ImpBookPostEntry model) => this.Build(model);

        public string GetLocation(ImpBookPostEntry model)
        {
            throw new System.NotImplementedException();
        }
    }
}