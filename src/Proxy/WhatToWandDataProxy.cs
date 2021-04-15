namespace Linn.Stores.Proxy
{
    using System.Collections.Generic;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Tpk.Models;

    public class WhatToWandDataProxy : IWhatToWandService
    {
        private readonly IDatabaseService databaseService;

        public WhatToWandDataProxy(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public IEnumerable<WhatToWandLine> WhatToWand(string fromLocation)
        {
            // todo - this query isn't right - how do I incorporate fromLocation?
            var sql = $@"select
            SOD.ORDER_NUMBER ORDER_NUMBER,
            SOD.ORDER_LINE ORDER_LINE,
            SA.ARTICLE_NUMBER ARTICLE_NUMBER, 
            SA.INVOICE_DESCRIPTION,
            '' manual, --THIS IS A PLACEHOLDER FOR MANUAL - TODO - IS THIS STILL NEEDED?
            which_mains_lead(sod.article_number, ad.country) lead_to_display,
            SOD.ORDER_LINE KITTED,
            SOD.QTY_ORDERED QTY_ORDERED,
            SOD.SUPPLY_IN_FULL_CODE SUPPLY_IN_FULL_CODE
            FROM SALES_ARTICLES SA,
            SALES_ACCOUNTS A,
            ADDRESSES AD,
            COUNTRIES CO,
            SALES_ORDERS SO,
            SALES_ORDER_DETAILS SOD,
            CONSIGNMENTS C,
            ADDRESSES AD2,
            REQUISITION_HEADERS RH,
            REQUISITION_LINES RL,
            REQ_MOVES RM
            WHERE C.CONSIGNMENT_ID=RH.DOCUMENT_1
            AND C.ADDRESS_ID=AD2.ADDRESS_ID
            AND c.status='L'
            AND SOD.ORDER_NUMBER = SO.ORDER_NUMBER
            AND SO.ACCOUNT_ID = A.ACCOUNT_ID
            AND SO.DELIVERY_ADDRESS_ID = AD.ADDRESS_ID
            AND AD.COUNTRY = CO.COUNTRY_CODE
            AND SOD.ARTICLE_NUMBER = SA.ARTICLE_NUMBER
            AND RH.BOOKED = 'N'
            AND RH.CANCELLED='N'
            AND RH.DOC1_NAME='CONS'
            AND (
            		    (RH.FUNCTION_CODE = 'INVOICE')
            		    OR
            		    (RH.FUNCTION_CODE = 'ALLOC' AND RM.DATE_BOOKED IS NULL
            --RM.BOOKED='N'
            )
            		    )
            AND RH.REQ_NUMBER=RL.REQ_NUMBER
            AND RL.DOCUMENT_1=SOD.ORDER_NUMBER
            AND RL.DOCUMENT_1_LINE=SOD.ORDER_LINE
            AND RL.NAME='O'
            AND RL.CANCELLED='N'
            AND RL.REQ_NUMBER=RM.REQ_NUMBER
            AND RL.LINE_NUMBER=RM.LINE_NUMBER
            AND NVL(RM.QTY_PRINTED,0)<RM.QTY 
            --NEED TO CHECK TOTAL QTIES!
            GROUP BY
            C.CONSIGNMENT_ID,
            AD2.ADDRESSEE,
            AD.ADDRESSEE,ad.country,
            SO.ACCOUNT_ID,
            A.ACCOUNT_NAME,
            CO.DISPLAY_NAME,
            CO.MAINS_LEAD,
            SOD.ORDER_NUMBER,
            SOD.ORDER_LINE,
            SA.ARTICLE_NUMBER,
            SA.INVOICE_DESCRIPTION,
            SOD.QTY_ORDERED,
            SO.DELIVERY_ADDRESS_ID,
            SO.CURRENCY_CODE,
            SOD.COMMENTS,
            SOD.SUPPLY_IN_FULL_CODE,
            SOD.INTERNAL_COMMENTS,
            SOD.NETT_TOTAL,
            SOD.PROMOTION_CODE,
            which_mains_lead(sod.article_number, ad.country) 
            order by c.consignment_id, sod.order_number,SOD.SUPPLY_IN_FULL_CODE,sod.order_line";

            var rows = this.databaseService.ExecuteQuery(sql).Tables[0].Rows;
            var result = new List<WhatToWandLine>();

            for (var i = 0; i < rows.Count; i++)
            {
                var data = rows[i].ItemArray;
                result.Add(new WhatToWandLine
                              {
                                  OrderNumber = int.Parse(data[0].ToString()),
                                  OrderLine = int.Parse(data[1].ToString()),
                                  ArticleNumber = data[2].ToString(),
                                  InvoiceDescription = data[3].ToString(),
                                  Manual = data[4].ToString(),
                                  MainsLead = data[5].ToString(),
                                  Kitted = int.Parse(data[6].ToString()),
                                  Ordered = int.Parse(data[7].ToString()),
                                  Sif = data[8].ToString()
                });
            }

            return result;
        }
    }
}
