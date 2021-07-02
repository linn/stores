namespace Linn.Stores.Proxy
{
    using System.Data;

    using Linn.Common.Proxy.LinnApps;
    using Linn.Stores.Domain.LinnApps.ExternalServices;

    using Oracle.ManagedDataAccess.Client;

    public class GoodsInPack : IGoodsInPack
    {
        private readonly IDatabaseService databaseService;

        public GoodsInPack(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public void DoBookIn(
            int bookInRef,
            string transactionType,
            int createdBy,
            string partNumber,
            int qty,
            int orderNumber,
            int orderLine,
            int rsnNumber,
            string storagePlace,
            string storageType,
            string demLocation,
            string state,
            string comments,
            string condition,
            string rsnAccessories,
            int reqNumber,
            out bool success)
        {
            throw new System.NotImplementedException();
        }

        public string GetErrorMessage()
        {
            using (var connection = this.databaseService.GetConnection())
            {
                connection.Open();
                var cmd =
                    new OracleCommand(
                        "goods_in_pack.GET_ERR_MESS",
                        connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };

                var result = new OracleParameter("G_ERR", OracleDbType.Varchar2)
                                 {
                                     Direction = ParameterDirection.ReturnValue,
                                     Size = 50
                                 };
                cmd.Parameters.Add(result);

                cmd.ExecuteNonQuery();
                connection.Close();
                return result.Value.ToString();
            }
        }

        public void GetPurchaseOrderDetails(
            int orderNumber,
            int orderLine,
            out string partNumber,
            out string description,
            out string uom,
            out int orderQty,
            out string qualityControlPart,
            out string manufacturerPartNumber,
            out string docType,
            out string message)
        {
            using (var connection = this.databaseService.GetConnection())
            {
                connection.Open();
                var cmd = new OracleCommand("goods_in_pack.get_po_details", connection)
                              {
                                  CommandType = CommandType.StoredProcedure
                              };

                var arg1 = new OracleParameter("p_order_number", OracleDbType.Int32)
                               {
                                   Direction = ParameterDirection.Input,
                                   Size = 50,
                                   Value = orderNumber
                               };
                cmd.Parameters.Add(arg1);

                var arg2 = new OracleParameter("p_order_line", OracleDbType.Int32)
                               {
                                   Direction = ParameterDirection.Input,
                                   Size = 50,
                                   Value = orderLine
                               };
                cmd.Parameters.Add(arg2);

                var partNumberParam = new OracleParameter("p_part_number", OracleDbType.Varchar2)
                                           {
                                               Direction = ParameterDirection.Output,
                                               Size = 50
                                           };
                cmd.Parameters.Add(partNumberParam);

                var partDescriptionParam = new OracleParameter("p_description", OracleDbType.Varchar2)
                                          {
                                              Direction = ParameterDirection.Output,
                                              Size = 200
                                          };
                cmd.Parameters.Add(partDescriptionParam);

                var uomParam = new OracleParameter("p_uom", OracleDbType.Varchar2)
                                               {
                                                   Direction = ParameterDirection.Output,
                                                   Size = 50
                                               };
                cmd.Parameters.Add(uomParam);

                var orderQtyParam = new OracleParameter("p_order_qty", OracleDbType.Int32)
                                             {
                                                 Direction = ParameterDirection.Output
                                             };
                cmd.Parameters.Add(orderQtyParam);

                var qualityControlPartParam 
                    = new OracleParameter("p_qc_part", OracleDbType.Varchar2)
                                   {
                                       Direction = ParameterDirection.Output,
                                       Size = 50
                                   };
                cmd.Parameters.Add(qualityControlPartParam);

                var manufacturerPartParam = new OracleParameter("p_manuf_part_number", OracleDbType.Varchar2)
                                          {
                                              Direction = ParameterDirection.Output,
                                              Size = 50
                                          };
                cmd.Parameters.Add(manufacturerPartParam);

                var docTypeParam = new OracleParameter("p_doc_type", OracleDbType.Varchar2)
                                                {
                                                    Direction = ParameterDirection.Output,
                                                    Size = 50
                                                };
                cmd.Parameters.Add(docTypeParam);

                var messageParam = new OracleParameter("p_error_mess", OracleDbType.Varchar2)
                                       {
                                           Direction = ParameterDirection.Output,
                                           Size = 50
                                       };
                cmd.Parameters.Add(messageParam);

                cmd.ExecuteNonQuery();
                connection.Close();

                partNumber = partNumberParam.Value.ToString();
                description = partDescriptionParam.Value.ToString();
                uom = uomParam.Value.ToString();
                orderQty = int.Parse(orderQtyParam.Value.ToString());
                qualityControlPart = qualityControlPartParam.Value.ToString();
                manufacturerPartNumber = manufacturerPartParam.Value.ToString();
                docType = docTypeParam.Value.ToString();
                message = messageParam.Value.ToString();
            }
        }

        public bool PartHasStorageType(string partNumber, out int bookInLocation, out string kardex, out bool newPart)
        {
            using (var connection = this.databaseService.GetConnection())
            {
                connection.Open();
                var cmd = new OracleCommand("goods_in_pack.part_has_storage_type", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                var partNumberParam = new OracleParameter("p_part_number", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Size = 50
                };
                cmd.Parameters.Add(partNumberParam);

                var bookInLocationParameter = new OracleParameter("p_bookin_loc", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(bookInLocationParameter);

                var kardexParam = new OracleParameter("p_kardex", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Output,
                    Size = 50
                };
                cmd.Parameters.Add(kardexParam);

                var newPartParam = new OracleParameter("p_new_part", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(newPartParam);

                var result = new OracleParameter("result", OracleDbType.Int32)
                                 {
                                     Direction = ParameterDirection.ReturnValue
                                 };
                cmd.Parameters.Add(result);

                cmd.ExecuteNonQuery();

                bookInLocation = int.Parse(bookInLocationParameter.Value.ToString());
                kardex = kardexParam.Value.ToString();
                newPart = int.Parse(newPartParam.Value.ToString()) == 0;
                connection.Close();

                return int.Parse(result.Value.ToString()) == 0;
            }
        }
    }
}
