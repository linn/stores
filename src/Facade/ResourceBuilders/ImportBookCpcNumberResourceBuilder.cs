namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.ImportBooks;

    public class ImportBookCpcNumberResourceBuilder : IResourceBuilder<ImportBookCpcNumber>
    {
        public ImportBookCpcNumberResource Build(ImportBookCpcNumber model)
        {
            return new ImportBookCpcNumberResource
                       {
                           CpcNumber = model.CpcNumber, 
                           Description = model.Description,
                           DateInvalid = model.DateInvalid?.ToString("o")
                       };
        }

        object IResourceBuilder<ImportBookCpcNumber>.Build(ImportBookCpcNumber model) => this.Build(model);

        public string GetLocation(ImportBookCpcNumber model)
        {
            throw new System.NotImplementedException();
        }
    }
}
