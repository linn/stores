namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.Parts;

    public class ImportBookPostEntryResourceBuilder : IResourceBuilder<ImportBookPostEntry>
    {
        public ImportBookPostEntryResource Build(ImportBookPostEntry model)
        {
            return new ImportBookPostEntryResource
                       {
                           ImportBookId = model.ImportBookId,
                           LineNumber = model.LineNumber,
                           EntryCodePrefix = model.EntryCodePrefix,
                           EntryCode = model.EntryCode,
                           EntryDate = model.EntryDate?.ToString("o"),
                           Reference = model.Reference,
                           Duty = model.Duty,
                           Vat = model.Vat
                       };
        }

        object IResourceBuilder<ImportBookPostEntry>.Build(ImportBookPostEntry model) => this.Build(model);

        public string GetLocation(ImportBookPostEntry model)
        {
            throw new System.NotImplementedException();
        }
    }
}
