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
                           WhatToWandReport = tpkResult.Report != null ? new WhatToWandResource
                                                  {
                                                      Account = new SalesAccountResource
                                                                    {
                                                                        AccountId = tpkResult.Report.Account.AccountId,
                                                                        AccountName = tpkResult.Report.Account.AccountName
                                                                    },
                                                      Type = tpkResult.Report.Type,
                                                      Consignment = new ConsignmentResource
                                                                        {
                                                                            AddressId = tpkResult.Report.Consignment.AddressId,
                                                                            ConsignmentId = tpkResult.Report.Consignment.ConsignmentId,
                                                                            Country = tpkResult.Report.Consignment.Country?.DisplayName,
                                                                            SalesAccountId = tpkResult.Report.Consignment.SalesAccountId,
                                                                            TotalNettValue = tpkResult.Report.TotalNettValueOfConsignment,
                                                                            CurrencyCode = tpkResult.Report.CurrencyCode
                                                                        },
                                                      Lines = tpkResult.Report.Lines
                                                          .Select(l => new WhatToWandLineResource
                                                                           {
                                                                               ArticleNumber = l.ArticleNumber,
                                                                               InvoiceDescription = l.InvoiceDescription,
                                                                               Kitted = l.Kitted,
                                                                               MainsLead = l.MainsLead,
                                                                               Manual = l.Manual,
                                                                               OrderLine = l.OrderLine,
                                                                               OrderNumber = l.OrderNumber,
                                                                               Ordered = l.Ordered,
                                                                               Sif = l.Sif
                                                                           })
                                                  } 
                                                  : null
                       };
        }

        public string GetLocation(TpkResult tpkResult)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<TpkResult>.Build(TpkResult tpkResult) => this.Build(tpkResult);
    }
}
