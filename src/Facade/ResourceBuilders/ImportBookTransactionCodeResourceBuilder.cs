namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.ImportBooks;

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

        public string GetLocation(ImportBookTransactionCode model)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<ImportBookTransactionCode>.Build(ImportBookTransactionCode model)
        {
            return this.Build(model);
        }
    }
}
