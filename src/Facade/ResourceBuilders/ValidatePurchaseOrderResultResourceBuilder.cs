namespace Linn.Stores.Facade.ResourceBuilders
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;


    public class ValidatePurchaseOrderResultResourceBuilder : IResourceBuilder<ValidatePurchaseOrderResult>
    {
        public ValidatePurchaseOrderResultResource Build(ValidatePurchaseOrderResult model)
        {
            return new ValidatePurchaseOrderResultResource
                       {
                             OrderNumber = model.OrderNumber,
                             OrderLine = model.OrderLine,
                             QtyBookedIn = model.QtyBookedIn,
                             BookInMessage = model.BookInMessage,
                             DocumentType = model.DocumentType,
                             ManufacturersPartNumber = model.ManufacturersPartNumber,
                             OrderQty = model.OrderQty,
                             OrderUnitOfMeasure = model.OrderUnitOfMeasure,
                             PartDescription = model.PartDescription,
                             PartNumber = model.PartNumber,
                             QcPart = model.QcPart,
                             PartQcWarning = model.PartQcWarning,
                             Storage = model.Storage,
                             TransactionType = model.TransactionType,
                             State = model.State
                       };
        }

        object IResourceBuilder<ValidatePurchaseOrderResult>.Build(ValidatePurchaseOrderResult model) => this.Build(model);

        public string GetLocation(ValidatePurchaseOrderResult model)
        {
            throw new System.NotImplementedException();
        }
    }
}
