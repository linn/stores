namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.ImportBooks;

    public class ImportBookTransportCodeResourceBuilder : IResourceBuilder<ImportBookTransportCode>
    {
        public ImportBookTransportCodeResource Build(ImportBookTransportCode model)
        {
            return new ImportBookTransportCodeResource
            {
                TransportId = model.TransportId,
                Description = model.Description
            };
        }

        object IResourceBuilder<ImportBookTransportCode>.Build(ImportBookTransportCode model) => this.Build(model);

        public string GetLocation(ImportBookTransportCode model)
        {
            throw new System.NotImplementedException();
        }
    }
}
