﻿namespace Linn.Stores.Proxy
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Proxy.LinnApps;
    using Linn.Stores.Domain.LinnApps.ConsignmentShipfiles;
    using Linn.Stores.Domain.LinnApps.ExternalServices;

    public class ConsignmentShipfileDataProxy : IConsignmentShipfileDataService
    {
        private readonly IDatabaseService databaseService;

        public ConsignmentShipfileDataProxy(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public ConsignmentShipfilePdfModel GetPdfModelData(int consignmentId, int addressId)
        {
            var sql = $@"
            select CONS.CONSIGNMENT_ID CONSIGNMENT_ID,
            DATE_CLOSED DESPATCH_DATE,
            CONS.ADDRESS ADDRESS,
            CARR.NAME CARRIER, CONSIGNMENT.CUSTOMER_REFS(CONSIGNMENT_ID) Ref,
            ADDV.ADDRESS OUTLET_ADDRESS,
            CARRIER_REF 
            from V_CONSIGNMENT_ADDRESS CONS,
            CARRIERS CARR,
            ADDRESSES_VIEW ADDV
            WHERE cons.CONSIGNMENT_ID = {consignmentId}
            AND CONS.CARRIER = CARR.CARRIER_CODE
            AND ADDV.ADDRESS_ID = {addressId}";
            var rows = this.databaseService.ExecuteQuery(sql).Tables[0].Rows;
            
            var data = rows[0].ItemArray;
            var result = new ConsignmentShipfilePdfModel
                             {
                                 ConsignmentNumber = data[0].ToString(),
                                 DateDispatched = data[1].ToString(),
                                 Address = data[2].ToString().Split("\n"),
                                 Carrier = data[3].ToString(),
                                 Reference = data[4].ToString(),
                                 OutletAddress = data[5].ToString().Split("\n"),
                                 CarriersReference = data[6].ToString(),
                                 PackingList = this.GetPackingListData(consignmentId).ToArray(),
                                 DespatchNotes = this.GetDespatchNote(consignmentId).ToArray()
                             };
            return result;
        }

        private IEnumerable<PackingListItem> GetPackingListData(int consignmentId)
        {
            var sql = $@"
            (SELECT CI.CONSIGNMENT_ID,
            PALLET_NO,
            NVL(CI.CONTAINER_NO,999998) CONTAINER_NO,
            NVL(consignment.container_description(CI.CONSIGNMENT_ID,CI.CONTAINER_NO),
            TO_CHAR(QTY)||' '||ITEM_DESCRIPTION) ITEM_DESCRIPTION,
            QTY
            FROM
            CONSIGNMENT_ITEMS CI
            WHERE CONSIGNMENT_ID = {consignmentId}
            AND PALLET_NO IS NOT NULL
            UNION ALL
            SELECT CI.CONSIGNMENT_ID,
            PALLET.PALLET_NO,
            CI.CONTAINER_NO,
            NVL(consignment.container_description(CI.CONSIGNMENT_ID,CI.CONTAINER_NO),
            TO_CHAR(QTY)||' '||ITEM_DESCRIPTION) ITEM_DESCRIPTION,
            QTY
            FROM
            CONSIGNMENT_ITEMS CI,
            (SELECT DISTINCT PALLET_NO,CONTAINER_NO
            FROM CONSIGNMENT_ITEMS
            WHERE CONSIGNMENT_ID = {consignmentId}
            AND PALLET_NO IS NOT NULL) PALLET
            WHERE CONSIGNMENT_ID = {consignmentId}
            AND CI.CONTAINER_NO = PALLET.CONTAINER_NO
            AND CI.PALLET_NO IS NULL
            UNION ALL
            SELECT CI.CONSIGNMENT_ID,
            PALLET_NO,
            CI.CONTAINER_NO,
            NVL(consignment.container_description(CI.CONSIGNMENT_ID,CI.CONTAINER_NO),
            TO_CHAR(QTY)||' '||ITEM_DESCRIPTION) ITEM_DESCRIPTION,
            QTY
            FROM
            CONSIGNMENT_ITEMS CI
            WHERE CONSIGNMENT_ID = {consignmentId}
            AND PALLET_NO IS NULL
            AND NOT EXISTS(SELECT PALLET_NO FROM CONSIGNMENT_ITEMS
            WHERE PALLET_NO IS NOT NULL AND CONTAINER_NO = CI.CONTAINER_NO
            AND CONSIGNMENT_ID = CI.CONSIGNMENT_ID)
            UNION ALL
            SELECT  999999,999999,999999, '<<__ End of input __>>',0 from dual
            )
            ORDER BY CONTAINER_NO";

            var rows = this.databaseService.ExecuteQuery(sql).Tables[0].Rows;
            var result = new List<PackingListItem>();

            for (var i = 0; i < rows.Count; i++)
            {
                var data = rows[i].ItemArray;
                int.TryParse(data[2].ToString(), out var box);
                int.TryParse(data[1].ToString(), out var pallet);
                decimal.TryParse(data[4].ToString(), out var qty);
                var item = new PackingListItem(
                    pallet == 0 ? (int?)null : pallet,
                    box == 0 ? (int?)null : box,
                    data[3].ToString(),
                    qty);
                result.Add(item);
            }

            return result;
        }

        private IEnumerable<DespatchNote> GetDespatchNote(int consignmentId)
        {
            var sql = $@"
            SELECT INV.CONSIGNMENT_ID, INV.DOCUMENT_NUMBER DOC_NUMBER, 
            INV.DOCUMENT_DATE, INVI.INVOICE_LINE, INVD.DESCRIPTION,
            INVI.QTY, INVD.CUSTOMERS_ORDER_NOS,
            INVI.SALES_ORDER_NUMBER, INVI.ORDER_LINE
            FROM INVOICES INV, INVOICED_ITEMS INVI, 
            INVOICE_DETAILS INVD, SALES_OUTLETS SAOU
            WHERE  INV.DOCUMENT_TYPE='I'
            AND INV.CONSIGNMENT_ID = {consignmentId}
            AND (INVI.INVOICE_LINE=INVD.LINE_NO)
            AND (INVI.INVOICE_NUMBER=INVD.DOCUMENT_NUMBER)
            AND (INVD.OUTLET_NUMBER=SAOU.OUTLET_NUMBER)
            AND (INVD.ACCOUNT_ID=SAOU.ACCOUNT_ID)
            AND (INVD.DOCUMENT_TYPE=INV.DOCUMENT_TYPE)
            AND (INVD.DOCUMENT_NUMBER=INV.DOCUMENT_NUMBER)
            ORDER BY SAOU.NAME ASC, INV.DOCUMENT_NUMBER, INVI.INVOICE_LINE";
            var rows = this.databaseService.ExecuteQuery(sql).Tables[0].Rows;
            var result = new List<DespatchNote>();
            for (var i = 0; i < rows.Count; i++)
            {
                var data = rows[i].ItemArray;
                var orderNumber = data[7].ToString();
                var orderLine = data[8].ToString();
                result.Add(new DespatchNote 
                               {
                                    ConsignmentId = consignmentId.ToString(),
                                    DocNumber = data[1].ToString(),
                                    DocumentDate = data[2].ToString(),
                                    InvoiceLine = data[3].ToString(),
                                    Description = data[4].ToString(),
                                    Quantity = data[5].ToString(),
                                    CustomersOrderNumbers = data[6].ToString(),
                                    SalesOrderNumber = orderNumber,
                                    OrderLine = orderLine,
                                    SerialNumbers = this.GetSerialNumbers(
                                        consignmentId, 
                                        int.Parse(orderNumber), 
                                        int.Parse(orderLine))
                                        .Aggregate((acc, c) => acc + " " + c)
                });
            }

            return result;
        }

        private IEnumerable<string> GetSerialNumbers(int consignmentId, int orderNumber, int orderLine)
        {
            var sql = $@"
            SELECT CI.SERIAL_NUMBER
            FROM CONSIGNMENT_ITEMS CI WHERE CI.ORDER_NUMBER = {orderNumber}
            AND CI.ORDER_LINE = {orderLine}
            AND CI.CONSIGNMENT_ID = {consignmentId}";
            var rows = this.databaseService.ExecuteQuery(sql).Tables[0].Rows;
            var result = new List<string>();
            for (var i = 0; i < rows.Count; i++)
            {
                var data = rows[i].ItemArray;
                result.Add(data[0].ToString());
            }

            return result;
        }
    }
}
