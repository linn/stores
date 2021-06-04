namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.Parts;

    public class ImportBookTransactionCodeResourceBuilder : IResourceBuilder<ImportBookTransactionCode>
    {
        public ImportBookTransactionCodeResource Build(ImportBookTransactionCode model)
        {
            return new ImportBookTransactionCodeResource
            {
                TransactionId = model.TransactionId,
                Description = model.Description
            };
        }

        object IResourceBuilder<ImportBookTransactionCode>.Build(ImportBookTransactionCode model) => this.Build(model);

        public string GetLocation(ImportBookTransactionCode model)
        {
            throw new System.NotImplementedException();
        }
    }
}
