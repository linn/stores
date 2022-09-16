namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Tpk.Models;
    using Linn.Stores.Resources;
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
                               ?.Select(s 
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
                           WhatToWandReport = tpkResult.Consignments?.Select(c =>
                               new WhatToWandConsignmentResource
                                   {
                                       Lines = c.Lines.Select(l => 
                                           new WhatToWandLineResource
                                               {
                                                   OrderNumber = l.OrderNumber,
                                                   OrderLine = l.OrderLine,
                                                   ArticleNumber = l.ArticleNumber,
                                                   InvoiceDescription = l.InvoiceDescription,
                                                   MainsLead = l.MainsLead,
                                                   Kitted = l.Kitted,
                                                   Ordered = l.Ordered,
                                                   Sif = l.Sif,
                                                   SerialNumberComments = l.SerialNumberComments
                                               }),
                                       Consignment = new TpkConsignmentResource
                                                     {
                                                         AddressId = c.Consignment.AddressId,
                                                         ConsignmentId = c.Consignment.ConsignmentId,
                                                         CountryCode = c.Consignment.Address?.Country?.CountryCode,
                                                         Country = c.Consignment.Address?.Country?.DisplayName,
                                                         SalesAccountId = c.Consignment.SalesAccountId,
                                                         TotalNettValue = c.TotalNettValueOfConsignment,
                                                         CurrencyCode = c.CurrencyCode
                                                     },
                                        Account = new SalesAccountResource
                                                     {
                                                         AccountId = c.Account.AccountId,
                                                         AccountName = c.Account.AccountName
                                                     },
                                       Type = c.Type,
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
