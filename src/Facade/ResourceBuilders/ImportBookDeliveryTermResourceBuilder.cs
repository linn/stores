namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.ImportBooks;

    public class ImportBookDeliveryTermResourceBuilder : IResourceBuilder<ImportBookDeliveryTerm>
    {
        public ImportBookDeliveryTermResource Build(ImportBookDeliveryTerm model)
        {
            return new ImportBookDeliveryTermResource
                       {
                           DeliveryTermCode = model.DeliveryTermCode,
                           Description = model.Description,
                           Comments = model.Comments,
                           SortOrder = model.SortOrder
                       };
        }

        object IResourceBuilder<ImportBookDeliveryTerm>.Build(ImportBookDeliveryTerm model) => this.Build(model);

        public string GetLocation(ImportBookDeliveryTerm model)
        {
            throw new System.NotImplementedException();
        }
    }
}
