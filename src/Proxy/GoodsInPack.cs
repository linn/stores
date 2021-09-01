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
            int? orderNumber,
            int? orderLine,
            int? loanNumber,
            int? loanLine,
            int? rsnNumber,
            string storagePlace,
            string storageType,
            string demLocation,
            string state,
            string comments,
            string condition,
            string rsnAccessories,
            out int? reqNumber,
            out bool success)
        {
            using (var connection = this.databaseService.GetConnection())
            {
                connection.Open();
                var cmd =
                    new OracleCommand(
                        "goods_in_pack.do_bookin_wrapper",
                        connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                var bookinRefParam = new OracleParameter("p_bookin_ref", OracleDbType.Int32)
                               {
                                   Direction = ParameterDirection.Input,
                                   Value = bookInRef
                               };
                cmd.Parameters.Add(bookinRefParam);

                var transTypeParam = new OracleParameter("p_trans_type", OracleDbType.Varchar2)
                               {
                                   Direction = ParameterDirection.Input,
                                   Size = 50,
                                   Value = transactionType
                               };
                cmd.Parameters.Add(transTypeParam);

                var createdByParam = new OracleParameter("p_created_by", OracleDbType.Int32)
                               {
                                   Direction = ParameterDirection.Input,
                                   Value = createdBy
                               };
                cmd.Parameters.Add(createdByParam);

                var partNumberParam = new OracleParameter("p_part_number", OracleDbType.Varchar2)
                                         {
                                             Direction = ParameterDirection.Input,
                                             Size = 50,
                                             Value = partNumber
                                         };
                cmd.Parameters.Add(partNumberParam);

                var qtyParam = new OracleParameter("p_qty", OracleDbType.Int32)
                                         {
                                             Direction = ParameterDirection.Input,
                                             Value = qty
                                         };
                cmd.Parameters.Add(qtyParam);

                var orderNumberParam = new OracleParameter("p_order_number", OracleDbType.Int32)
                                   {
                                       Direction = ParameterDirection.Input,
                                       Value = orderNumber
                                   };
                cmd.Parameters.Add(orderNumberParam);

                var orderLineParam = new OracleParameter("p_order_line", OracleDbType.Int32)
                                           {
                                               Direction = ParameterDirection.Input,
                                               Value = orderLine
                                           };
                cmd.Parameters.Add(orderLineParam);

                var loanNumberParam = new OracleParameter("p_loan_number", OracleDbType.Int32)
                                         {
                                             Direction = ParameterDirection.Input,
                                             Value = loanNumber
                                         };
                cmd.Parameters.Add(loanNumberParam);

                var loanLineParam = new OracleParameter("p_loan_line", OracleDbType.Int32)
                                          {
                                              Direction = ParameterDirection.Input,
                                              Value = loanLine
                                          };
                cmd.Parameters.Add(loanLineParam);

                var rsnNumberParam = new OracleParameter("p_rsn_number", OracleDbType.Int32)
                                            {
                                                Direction = ParameterDirection.Input,
                                                Value = rsnNumber
                                            };
                cmd.Parameters.Add(rsnNumberParam);

                var storagePlaceParam = new OracleParameter("p_storage_place", OracleDbType.Varchar2)
                                          {
                                              Direction = ParameterDirection.Input,
                                              Size = 50,
                                              Value = storagePlace
                                          };
                cmd.Parameters.Add(storagePlaceParam);

                var storageTypeParam = new OracleParameter("p_storage_type", OracleDbType.Varchar2)
                                            {
                                                Direction = ParameterDirection.Input,
                                                Size = 50,
                                                Value = storageType
                                            };
                cmd.Parameters.Add(storageTypeParam);

                var demLocationParam = new OracleParameter("p_dem_location", OracleDbType.Varchar2)
                                           {
                                               Direction = ParameterDirection.Input, Size = 50, Value = demLocation
                                           };
                cmd.Parameters.Add(demLocationParam);

                var stateParam = new OracleParameter("p_state", OracleDbType.Varchar2)
                                           {
                                               Direction = ParameterDirection.Input,
                                               Size = 50,
                                               Value = state
                                           };
                cmd.Parameters.Add(stateParam);

                var commentsParam = new OracleParameter("p_comments", OracleDbType.Varchar2)
                                     {
                                         Direction = ParameterDirection.Input,
                                         Size = 2000,
                                         Value = comments
                                     };
                cmd.Parameters.Add(commentsParam);

                var rsnConditionParam = new OracleParameter("p_rsn_condition", OracleDbType.Varchar2)
                                        {
                                            Direction = ParameterDirection.Input,
                                            Size = 2000,
                                            Value = condition
                                        };
                cmd.Parameters.Add(rsnConditionParam);

                var rsnAccessoriesParam = new OracleParameter("p_rsn_accs", OracleDbType.Varchar2)
                                            {
                                                Direction = ParameterDirection.Input,
                                                Size = 2000,
                                                Value = rsnAccessories
                                            };
                cmd.Parameters.Add(rsnAccessoriesParam);

                var reqNumberParam = new OracleParameter("p_req_number", OracleDbType.Int32)
                                         {
                                             Direction = ParameterDirection.Input,
                                         };
                cmd.Parameters.Add(reqNumberParam);

                var successParam = new OracleParameter("p_success_int", OracleDbType.Int32)
                                         {
                                             Direction = ParameterDirection.InputOutput,
                                             Value = 1
                                         };
                cmd.Parameters.Add(successParam);
               
                cmd.ExecuteNonQuery();
                var successInt = int.Parse(successParam.Value.ToString());

                success = successInt == 0;
                if (int.TryParse(reqNumberParam.Value.ToString(), out var reqNumberResult))
                {
                    reqNumber = reqNumberResult;
                }
                else
                {
                    reqNumber = null;
                }

                connection.Close();
            }
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

                partNumber = partNumberParam.Value.ToString().Equals("null") ? null : partNumberParam.Value.ToString();
                description = partDescriptionParam.Value.ToString().Equals("null") ? null : partDescriptionParam.Value.ToString();
                uom = uomParam.Value.ToString().Equals("null") ? null : uomParam.Value.ToString();
                int.TryParse(orderQtyParam.Value.ToString(), out orderQty);
                qualityControlPart = qualityControlPartParam.Value.ToString().Equals("null") 
                                         ? null 
                                         : qualityControlPartParam.Value.ToString();
                manufacturerPartNumber = manufacturerPartParam.Value.ToString().Equals("null") 
                                             ? null 
                                             : manufacturerPartParam.Value.ToString();
                docType = docTypeParam.Value.ToString().Equals("null") ? null : docTypeParam.Value.ToString();
                message = messageParam.Value.ToString().Equals("null") ? null : messageParam.Value.ToString();
            }
        }

        public bool PartHasStorageType(string partNumber, out int bookInLocation, out string kardex, out bool newPart)
        {
            using (var connection = this.databaseService.GetConnection())
            {
                connection.Open();
                var cmd = new OracleCommand("goods_in_pack.part_has_storage_type_wrapper", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                var arg1 = new OracleParameter("p_part_number", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Size = 200,
                    Value = partNumber
                };
                

                var bookInLocationParameter = new OracleParameter("p_bookin_loc", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Output
                };

                var kardexParam = new OracleParameter("p_kardex", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Output,
                    Size = 200
                };

                var newPartParam = new OracleParameter("p_new_part", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Output
                };
                

                var result = new OracleParameter("result", OracleDbType.Int32)
                                 {
                                     Direction = ParameterDirection.ReturnValue
                                 };

                cmd.Parameters.Add(result);
                cmd.Parameters.Add(arg1);
                cmd.Parameters.Add(bookInLocationParameter);
                cmd.Parameters.Add(kardexParam);
                cmd.Parameters.Add(newPartParam);

                cmd.ExecuteNonQuery();

                int.TryParse(bookInLocationParameter.Value.ToString(), out bookInLocation);
                kardex = kardexParam.Value.ToString();
                int.TryParse(newPartParam.Value.ToString(), out var newPartInt);
                newPart = newPartInt == 0;
                
                connection.Close();
                return int.Parse(result.Value.ToString()) == 0;
            }
        }

        public int GetNextBookInRef()
        {
            return this.databaseService.GetIdSequence("bookin_seq");
        }

        public int GetNextLogId()
        {
            return this.databaseService.GetIdSequence("gilog_seq");
        }

        public void GetKardexLocations(
            int? orderNumber,
            string docType,
            string partNumber,
            string storageType,
            out int? locationId,
            out string locationCode,
            int? qty)
        {
            using (var connection = this.databaseService.GetConnection())
            {
                connection.Open();
                var cmd = new OracleCommand("goods_in_pack.get_kardex_locations", connection)
                              {
                                  CommandType = CommandType.StoredProcedure
                              };

                var arg1 = new OracleParameter("p_order_number", OracleDbType.Int32)
                               {
                                   Direction = ParameterDirection.Input, Size = 50, Value = orderNumber
                               };
                cmd.Parameters.Add(arg1);

                var docTypeParameter = new OracleParameter("p_doc_type", OracleDbType.Varchar2)
                                          {
                                              Direction = ParameterDirection.Input,
                                              Size = 50,
                                              Value = docType
                                          };
                cmd.Parameters.Add(docTypeParameter);

                var partNumberParam = new OracleParameter("p_part_number", OracleDbType.Varchar2)
                                          {
                                              Direction = ParameterDirection.Input, 
                                              Size = 50,
                                              Value = partNumber
                                          };
                cmd.Parameters.Add(partNumberParam);

                var storageTypeParameter = new OracleParameter("p_storage_type", OracleDbType.Varchar2)
                                          {
                                              Direction = ParameterDirection.Input,
                                              Size = 50,
                                              Value = storageType
                                          };
                cmd.Parameters.Add(storageTypeParameter);

                var locationIdParameter = new OracleParameter("p_location_id", OracleDbType.Int32)
                                               {
                                                   Direction = ParameterDirection.Output,
                                                   Size = 50
                                               };
                
                cmd.Parameters.Add(locationIdParameter);

                var locationCodeParameter = new OracleParameter("p_location_code", OracleDbType.Varchar2)
                                               {
                                                   Direction = ParameterDirection.Output, 
                                                   Size = 50
                                               };
                cmd.Parameters.Add(locationCodeParameter);

                var qtyParameter = new OracleParameter("p_qty", OracleDbType.Int32)
                                          {
                                              Direction = ParameterDirection.Input,
                                              Size = 50,
                                              Value = qty
                                          };
                cmd.Parameters.Add(qtyParameter);

                cmd.ExecuteNonQuery();

                if (int.TryParse(locationIdParameter.Value.ToString(), out var result))
                {
                    locationId = result;
                }
                else
                {
                    locationId = null;
                }

                locationCode = locationCodeParameter.Value.ToString();
                connection.Close();
            }
        }

        public bool ParcelRequired(
            int? orderNumber,
            int? rsnNumber,
            int? loanNumber,
            out int? supplierId)
        {
            using (var connection = this.databaseService.GetConnection())
            {
                connection.Open();
                var cmd = new OracleCommand("goods_in_pack.parcel_required_wrapper", connection)
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

                var arg2 = new OracleParameter("p_rsn_number", OracleDbType.Int32)
                               {
                                   Direction = ParameterDirection.Input,
                                   Size = 50,
                                   Value = rsnNumber
                               };
                cmd.Parameters.Add(arg2);

                var arg3 = new OracleParameter("p_loan_number", OracleDbType.Int32)
                               {
                                   Direction = ParameterDirection.Input,
                                   Size = 50,
                                   Value = loanNumber
                               };
                cmd.Parameters.Add(arg3);

                var supplierIdParameter = new OracleParameter("p_supplier_id", OracleDbType.Int32)
                               {
                                   Direction = ParameterDirection.Output,
                                   Size = 50,
                               };
                cmd.Parameters.Add(supplierIdParameter);

                var result = new OracleParameter("result", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.ReturnValue
                };
                cmd.Parameters.Add(result);

                cmd.ExecuteNonQuery();

                connection.Close();

                if (int.TryParse(supplierIdParameter.Value.ToString(), out var s))
                {
                    supplierId = s;
                }
                else
                {
                    supplierId = null;
                }

                return int.Parse(result.Value.ToString()) == 0;
            }
        }
    }
}
