namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Tpk.Models;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.Tpk;

    public class WhatToWandConsignmentResourceBuilder : IResourceBuilder<WhatToWandConsignment>
    {
        public WhatToWandConsignmentResource Build(WhatToWandConsignment c)
        {
            return new WhatToWandConsignmentResource
                       {
                           Lines =
                               c.Lines.Select(
                                   l => new WhatToWandLineResource
                                            {
                                                OrderNumber = l.OrderNumber,
                                                OrderLine = l.OrderLine,
                                                ArticleNumber = l.ArticleNumber,
                                                InvoiceDescription = l.InvoiceDescription,
                                                MainsLead = l.MainsLead,
                                                Kitted = l.Kitted,
                                                Ordered = l.Ordered,
                                                Sif = l.Sif,
                                                SerialNumberComments = l.SerialNumberComments,
                                                Comments = l.Comments
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
                                             AccountId = c.Account.AccountId, AccountName = c.Account.AccountName
                                         },
                           Type = c.Type,
                       };
        }

        public string GetLocation(WhatToWandConsignment entity)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<WhatToWandConsignment>.Build(WhatToWandConsignment entity) => this.Build(entity);
    }
}
