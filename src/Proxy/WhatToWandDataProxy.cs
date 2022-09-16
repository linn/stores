namespace Linn.Stores.Proxy
{
    using System.Collections.Generic;

    using Linn.Common.Proxy.LinnApps;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Tpk.Models;

    public class WhatToWandDataProxy : IWhatToWandService
    {
        private readonly IDatabaseService databaseService;

        public WhatToWandDataProxy(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public bool ShouldPrintWhatToWand(string storagePlace)
        {
            var sql = $@"select count(distinct C.CONSIGNMENT_ID)
                        FROM CONSIGNMENTS C,
                          REQUISITION_HEADERS RH,
                          REQUISITION_LINES RL,
                          REQ_MOVES RM,
                          STOCK_LOCATORS SR,
                          STORAGE_LOCATIONS SL,
                          SALES_ORDER_DETAILS SOD,
                          V_STORAGE_PLACES STP
                        WHERE C.CONSIGNMENT_ID=RH.DOCUMENT_1
                        AND STP.STORAGE_PLACE = '{storagePlace}'
		                		AND NVL(SR.LOCATION_ID,-1)=NVL(STP.LOCATION_ID,-1)
		                		AND NVL(SR.PALLET_NUMBER,-1)=NVL(STP.PALLET_NUMBER,-1)
                        AND RH.DOC1_NAME='CONS'
                        AND (
                        (RH.FUNCTION_CODE = 'INVOICE'
                        AND RH.BOOKED = 'N'
                        AND RH.CANCELLED = 'N')
                        OR
                        (RH.FUNCTION_CODE = 'ALLOC'
                        AND RH.BOOKED = 'N'
                        AND RM.DATE_BOOKED IS NULL
                        AND RH.CANCELLED='N'
                        AND RL.CANCELLED='N'
                        )
                        )
                        AND RH.REQ_NUMBER=RL.REQ_NUMBER
                        AND RL.REQ_NUMBER=RM.REQ_NUMBER
                        AND RL.LINE_NUMBER=RM.LINE_NUMBER
                        AND RL.DOCUMENT_1=SOD.ORDER_NUMBER
                        AND RL.DOCUMENT_1_LINE=SOD.ORDER_LINE
                        AND RL.NAME='O'
                        AND RL.LINE_NUMBER=RM.LINE_NUMBER
                        AND RM.STOCK_LOCATOR_ID=SR.STOCK_LOCATOR_ID
                        AND SR.LOCATION_ID=SL.LOCATION_ID (+)
                        AND tpk_oo.wtw_type(C.CONSIGNMENT_ID)<>'*TPKD*'
                        AND C.STATUS='L'";
            
            var result = int.Parse(this.databaseService.ExecuteQuery(sql).Tables[0].Rows[0].ItemArray[0].ToString());

            return result > 0;
        }

        public IEnumerable<WhatToWandLine> WhatToWand(int? locationId, int? palletNumber)
        {
            var locationIdAnd =
                locationId.HasValue ? $"AND SR.LOCATION_ID ={locationId}" : "AND SR.LOCATION_ID IS NULL";
            var palletNumberAnd =
                palletNumber.HasValue ? $"AND SR.PALLET_NUMBER={palletNumber}" : "AND SR.PALLET_NUMBER IS NULL";
            var q1 = $@"select  
            SOD.ORDER_NUMBER ORDER_NUMBER,
            SOD.ORDER_LINE ORDER_LINE,
            SA.ARTICLE_NUMBER ARTICLE_NUMBER, 
            SA.INVOICE_DESCRIPTION,
            which_mains_lead(sod.article_number, ad.country) lead_to_display,
            SOD.ORDER_LINE KITTED,
            SOD.QTY_ORDERED QTY_ORDERED,
            SOD.SUPPLY_IN_FULL_CODE SUPPLY_IN_FULL_CODE,
            C.CONSIGNMENT_ID,
            SOD.INTERNAL_COMMENTS,
            substr(internal_comments,instr(internal_comments, ':' ) + 1 ,instr(internal_comments, '******',2) - 1 -instr(internal_comments, ':')) old_sernos,
            product_upgrade_pack.Get_Renew_Sernos_From_Original(to_number(substr(internal_comments,instr(internal_comments, ':' ) + 1 ,instr(internal_comments, '******',2) - 1 -instr(internal_comments, ':')))) renew_sernos 
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

            var q2 = $@"select  distinct C.CONSIGNMENT_ID
                        FROM CONSIGNMENTS C,
                        REQUISITION_HEADERS RH,
                        REQUISITION_LINES RL,
                        REQ_MOVES RM,
                        STOCK_LOCATORS SR
                        WHERE C.CONSIGNMENT_ID=RH.DOCUMENT_1
                        {palletNumberAnd}
                        AND SR.PALLET_NUMBER IS NOT NULL
                        AND RH.DOC1_NAME='CONS'
                        AND RL.PART_NUMBER = SR.PART_NUMBER 
                        AND (
                             (RH.FUNCTION_CODE = 'INVOICE'
                        AND RL.CANCELLED='N'
                        AND RH.CANCELLED='N'
                        AND RH.BOOKED = 'N')
                              OR
                              (RH.FUNCTION_CODE = 'ALLOC' 
                              AND RM.DATE_BOOKED IS NULL
                              --AND RM.BOOKED='N'
                        AND RL.CANCELLED='N'
                        AND RH.CANCELLED='N'
                        AND RH.BOOKED = 'N'
                        )
                              )
                         AND RH.REQ_NUMBER=RL.REQ_NUMBER
                         AND RL.REQ_NUMBER=RM.REQ_NUMBER
                         AND RL.LINE_NUMBER=RM.LINE_NUMBER
                         AND RM.STOCK_LOCATOR_ID=SR.STOCK_LOCATOR_ID
                         AND C.DATE_CLOSED IS NULL
                         AND c.status='L'
                         union all
                         select  distinct C.CONSIGNMENT_ID
                         FROM CONSIGNMENTS C,
                         REQUISITION_HEADERS RH,
                         REQUISITION_LINES RL,
                         REQ_MOVES RM,
                         STOCK_LOCATORS SR
                         WHERE C.CONSIGNMENT_ID=RH.DOCUMENT_1
                         {locationIdAnd}
                         AND SR.LOCATION_ID is not null
                         AND RH.DOC1_NAME='CONS'
                         AND RL.PART_NUMBER = SR.PART_NUMBER 
                         AND (
                              (RH.FUNCTION_CODE = 'INVOICE'
                         AND RL.CANCELLED='N'
                         AND RH.CANCELLED='N'
                         AND RH.BOOKED = 'N')
                              OR
                              (RH.FUNCTION_CODE = 'ALLOC' AND 
                               RM.DATE_BOOKED IS NULL
                               --RM.BOOKED='N'
                         AND RL.CANCELLED='N'
                         AND RH.CANCELLED='N'
                         AND RH.BOOKED = 'N'
                         )
                              )
                         AND RH.REQ_NUMBER=RL.REQ_NUMBER
                         AND RL.REQ_NUMBER=RM.REQ_NUMBER
                         AND RL.LINE_NUMBER=RM.LINE_NUMBER
                         AND RM.STOCK_LOCATOR_ID=SR.STOCK_LOCATOR_ID
                         AND C.DATE_CLOSED IS NULL
                         AND c.status='L'";

            var sql = $@"select * from ({q1}) t1 inner join ({q2}) t2 on t1.consignment_id = t2.consignment_id";
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
                                  MainsLead = data[4].ToString(),
                                  Kitted = int.Parse(data[5].ToString()),
                                  Ordered = int.Parse(data[6].ToString()),
                                  Sif = data[7].ToString(),
                                  ConsignmentId = int.Parse(data[8].ToString()),
                                  SerialNumberComments = data[9].ToString(),
                                  OldSernos = data[10].ToString(),
                                  RenewSernos = data[11].ToString()
                });
            }

            return result;
        }
    }
}
