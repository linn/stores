namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Tpk.Models;
    using Linn.Stores.Resources.Tpk;

    public class TpkResultResourceBuilder : IResourceBuilder<TpkResult>
    {
        public TpkResultResource Build(TpkResult tpkResult)
        {
            return new TpkResultResource
                       {
                           Message = tpkResult.Message,
                           Success = tpkResult.Success,
                           Transferred = tpkResult.Transferred
                               .Select(s 
                                   => new TransferredStockResource
                                          {
                                            FromLocation = s.FromLocation,
                                            ConsignmentId = s.ConsignmentId,
                                            Addressee = s.Addressee,
                                            ArticleNumber = s.ArticleNumber,
                                            DespatchLocationCode = s.DespatchLocationCode,
                                            InvoiceDescription = s.InvoiceDescription,
                                            LocationCode = s.LocationCode,
                                            LocationId = s.LocationId,
                                            OrderLine = s.OrderLine,
                                            OrderNumber = s.OrderNumber,
                                            PalletNumber = s.PalletNumber,
                                            Quantity = s.Quantity,
                                            ReqLine = s.ReqLine,
                                            ReqNumber = s.ReqNumber,
                                            StoragePlaceDescription = s.StoragePlaceDescription,
                                            VaxPallet = s.VaxPallet,
                                            Notes = s.Notes
                                        }),
                           WhatToWandReport = tpkResult.WhatToWand.Select(wtw => new WhatToWandLineResource
                               {
                                   Carrier = wtw.Carrier,
                                   ConsignmentId = wtw.ConsignmentId,
                                   ShippingMethod = wtw.ShippingMethod,
                                   Status = wtw.Status,
                                   Terms = wtw.Terms
                               })
                       };
        }

        public string GetLocation(TpkResult tpkResult)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<TpkResult>.Build(TpkResult tpkResult) => this.Build(tpkResult);
    }
}
