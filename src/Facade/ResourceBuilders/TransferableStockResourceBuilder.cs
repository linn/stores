namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Tpk;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.Tpk;

    public class TransferableStockResourceBuilder : IResourceBuilder<TransferableStock>
    {
        public TransferableStockResource Build(TransferableStock model)
        {
            return new TransferableStockResource
                       {
                           Addressee = model.Addressee,
                           ArticleNumber = model.ArticleNumber,
                           ConsignmentId = model.ConsignmentId,
                           DespatchLocationCode = model.DespatchLocationCode,
                           FromLocation = model.FromLocation,
                           InvoiceDescription = model.InvoiceDescription,
                           LocationCode = model.LocationCode,
                           LocationId = model.LocationId,
                           OrderLine = model.OrderLine,
                           OrderNumber = model.OrderNumber,
                           StoragrPlaceDescription = model.StoragrPlaceDescription,
                           PalletNumber = model.PalletNumber,
                           Quantity = model.PalletNumber,
                           ReqLine = model.ReqLine,
                           ReqNumber = model.ReqNumber,
                           VaxPallet = model.VaxPallet
                       };
        }

        public string GetLocation(TransferableStock model)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<TransferableStock>.Build(TransferableStock model) => this.Build(model);
    }
}
