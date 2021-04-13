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
            var sql =
                $@"select *
                FROM CONSIGNMENTS C,
                REQUISITION_HEADERS RH,
                REQUISITION_LINES RL,
                REQ_MOVES RM,
                STOCK_LOCATORS SR,
                STORAGE_LOCATIONS SL,
                SALES_ORDER_DETAILS SOD,
                V_STORAGE_PLACES STP
                WHERE C.CONSIGNMENT_ID=RH.DOCUMENT_1
                AND STP.STORAGE_PLACE = '{fromLocation}'
				AND NVL(SR.LOCATION_ID,-1)=NVL(STP.LOCATION_ID,-1)
				AND NVL(SR.PALLET_NUMBER,-1)=NVL(STP.PALLET_NUMBER,-1)
                AND RH.DOC1_NAME='CONS'
                AND ((RH.FUNCTION_CODE = 'INVOICE'
                AND RH.BOOKED = 'N'
                AND RH.CANCELLED = 'N')
                OR
                (RH.FUNCTION_CODE = 'ALLOC'
                AND RH.BOOKED = 'N'
                AND RM.DATE_BOOKED IS NULL
                AND RH.CANCELLED='N'
                AND RL.CANCELLED='N'))
                AND RH.REQ_NUMBER=RL.REQ_NUMBER
                AND RL.REQ_NUMBER=RM.REQ_NUMBER
                AND RL.LINE_NUMBER=RM.LINE_NUMBER
                AND RL.DOCUMENT_1=SOD.ORDER_NUMBER
                AND RL.DOCUMENT_1_LINE=SOD.ORDER_LINE
                AND RL.NAME='O'
                AND RL.LINE_NUMBER=RM.LINE_NUMBER
                AND RM.STOCK_LOCATOR_ID=SR.STOCK_LOCATOR_ID
                AND SR.LOCATION_ID=SL.LOCATION_ID (+)
                AND tpk_oo.wtw_type(C.CONSIGNMENT_ID)='*TPKD*' --fix me
                AND C.STATUS='L'";

            var rows = this.databaseService.ExecuteQuery(sql).Tables[0].Rows;
            var result = new List<WhatToWandLine>();

            for (var i = 0; i < rows.Count; i++)
            {
                var data = rows[i].ItemArray;
                result.Add(new WhatToWandLine
                              {
                                  // what data needs to go on the WTW report?
                              });
            }

            return result;
        }
    }
}
