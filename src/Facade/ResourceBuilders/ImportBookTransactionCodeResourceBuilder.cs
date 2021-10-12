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
                           Description = this.GetDescription(model.TransactionId, model.Description)
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

        private string GetDescription(int code, string dbDescription)
        {
            if (code == 10)
            {
                return "10 - Raw materials";
            }

            if (code == 20)
            {
                return "20 - Returned Goods";
            }

            return dbDescription;
        }
    }
}
