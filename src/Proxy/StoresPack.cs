namespace Linn.Stores.Proxy
{
    using System;
    using System.Data;

    using Linn.Common.Proxy.LinnApps;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Domain.LinnApps.StockMove.Models;

    using Oracle.ManagedDataAccess.Client;
    using Oracle.ManagedDataAccess.Types;

    public class StoresPack : IStoresPack
    {
        private readonly IDatabaseService databaseService;

        public StoresPack(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public ProcessResult UnallocateRequisition(int reqNumber, int? reqLineNumber, int userNumber)
        {
            var connection = this.databaseService.GetConnection();

            var cmd = new OracleCommand("stores_pack.unalloc_req_wrapper", connection)
                          {
                              CommandType = CommandType.StoredProcedure
                          };
            cmd.Parameters.Add(
                new OracleParameter("p_req_number", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Input,
                    Value = reqNumber
                });
            cmd.Parameters.Add(
                new OracleParameter("p_line_number", OracleDbType.Int32)
                    {
                        Direction = ParameterDirection.Input,
                        Value = reqLineNumber
                    });
            cmd.Parameters.Add(
                new OracleParameter("p_unalloc_by", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Input,
                    Value = userNumber
                });

            cmd.Parameters.Add(
                new OracleParameter("p_commit", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Input,
                    Value = 1
                });
            var messageParameter = new OracleParameter("p_message", OracleDbType.Varchar2)
                                       {
                                           Direction = ParameterDirection.InputOutput,
                                           Size = 4000
                                       };
            cmd.Parameters.Add(messageParameter);

            var successParameter = new OracleParameter("p_success", OracleDbType.Int32)
                                       {
                                           Direction = ParameterDirection.InputOutput
                                       };
            cmd.Parameters.Add(successParameter);

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

            return new ProcessResult
            {
                Message = messageParameter.Value.ToString(),
                Success = int.Parse(successParameter.Value.ToString()) == 1
            };
        }

        public void DoTpk(int? locationId, int? palletNumber, DateTime dateTimeStarted, out bool success)
        {
            success = false;
            using (var connection = this.databaseService.GetConnection())
            {
                connection.Open();
                var cmd = new OracleCommand("stores_oo.do_tpk_wrapper", connection)
                              {
                                  CommandType = CommandType.StoredProcedure
                              };

                var arg1 = new OracleParameter("p_location_id", OracleDbType.Int32)
                               {
                                   Direction = ParameterDirection.Input,
                                   Size = 50,
                                   Value = locationId
                               };
                cmd.Parameters.Add(arg1);

                var arg2 = new OracleParameter("p_pallet_number", OracleDbType.Int32)
                               {
                                   Direction = ParameterDirection.Input,
                                   Size = 50,
                                   Value = palletNumber
                               };
                cmd.Parameters.Add(arg2);

                var arg3 = new OracleParameter("p_date_started", OracleDbType.Date)
                               {
                                   Direction = ParameterDirection.Input,
                                   Value = dateTimeStarted
                               };
                cmd.Parameters.Add(arg3);

                var arg4 = new OracleParameter("p_success", OracleDbType.Int32)
                               {
                                   Direction = ParameterDirection.InputOutput,
                               };

                cmd.Parameters.Add(arg4);
                cmd.ExecuteNonQuery();

                success = int.Parse(arg4.Value.ToString()) == 1;

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
                        "stores_oo.STORES_ERR_MESS",
                        connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };

                var result = new OracleParameter("G_ERR", OracleDbType.Varchar2)
                                 {
                                     Direction = ParameterDirection.ReturnValue, Size = 50
                                 };
                cmd.Parameters.Add(result);

                cmd.ExecuteNonQuery();
                connection.Close();
                return result.Value.ToString();
            }
        }

        public RequisitionProcessResult CreateMoveReq(int userNumber)
        {
            var connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());

            var cmd = new OracleCommand("stores_pack.create_move_req", connection)
                          {
                              CommandType = CommandType.StoredProcedure
                          };

            cmd.Parameters.Add(new OracleParameter("p_created_by", OracleDbType.Int32)
                                   {
                                       Direction = ParameterDirection.Input, Value = userNumber
                                   });

            var reqNumberParameter = new OracleParameter("p_req_number", OracleDbType.Int32)
                                         {
                                             Direction = ParameterDirection.Output
                                         };
            cmd.Parameters.Add(reqNumberParameter);

            var messageParameter = new OracleParameter("p_message", OracleDbType.Varchar2)
                                       {
                                           Direction = ParameterDirection.Output,
                                           Size = 4000
                                       };
            cmd.Parameters.Add(messageParameter);

            var successParameter = new OracleParameter("p_success", OracleDbType.Int32)
                                       {
                                           Direction = ParameterDirection.Output
                                       };
            cmd.Parameters.Add(successParameter);

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

            return new RequisitionProcessResult
            {
                Message = messageParameter.Value.ToString(),
                Success = int.Parse(successParameter.Value.ToString()) == 1,
                ReqNumber = int.Parse(reqNumberParameter.Value.ToString())
            };
        }

        public RequisitionProcessResult CheckStockAtFromLocation(
            string partNumber,
            decimal quantity,
            string from,
            int? fromLocationId,
            int? fromPalletNumber,
            DateTime? fromStockDate)
        {
            var connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());
            var cmd = new OracleCommand("check_stock_at_from_location", connection)
                          {
                              CommandType = CommandType.StoredProcedure
                          };

            cmd.Parameters.Add(new OracleParameter("p_part_number", OracleDbType.Varchar2)
                                   {
                                       Direction = ParameterDirection.Input,
                                       Value = partNumber
                                   });
            cmd.Parameters.Add(new OracleParameter("p_qty", OracleDbType.Decimal)
                                   {
                                       Direction = ParameterDirection.Input,
                                       Value = quantity
                                   });
            cmd.Parameters.Add(new OracleParameter("p_from", OracleDbType.Varchar2)
                                   {
                                       Direction = ParameterDirection.Input,
                                       Value = from
                                   });
            cmd.Parameters.Add(new OracleParameter("p_from_location_id", OracleDbType.Int32)
                                   {
                                       Direction = ParameterDirection.Input,
                                       Value = fromLocationId
                                    });
            cmd.Parameters.Add(new OracleParameter("p_from_pallet_number", OracleDbType.Int32)
                                   {
                                       Direction = ParameterDirection.Input,
                                       Value = fromPalletNumber
                                   });
            cmd.Parameters.Add(new OracleParameter("p_from_stock_date", OracleDbType.Date)
                                   {
                                       Direction = ParameterDirection.Input, Value = fromStockDate
                                   });

            var stateParameter = new OracleParameter("p_state", OracleDbType.Varchar2)
                                     {
                                         Direction = ParameterDirection.Output,
                                         Size = 50
                                     };
            cmd.Parameters.Add(stateParameter);

            var messageParameter = new OracleParameter("p_message", OracleDbType.Varchar2)
                                       {
                                           Direction = ParameterDirection.Output,
                                           Size = 4000
                                       };
            cmd.Parameters.Add(messageParameter);

            var successParameter = new OracleParameter("p_success", OracleDbType.Int32)
                                       {
                                           Direction = ParameterDirection.Output
                                       };
            cmd.Parameters.Add(successParameter);

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

            return new RequisitionProcessResult
                       {
                           Message = messageParameter.Value.ToString(),
                           Success = int.Parse(successParameter.Value.ToString()) == 1,
                           State = stateParameter.Value?.ToString() == "null" ? null : stateParameter.Value?.ToString()
            };
        }

        public ProcessResult MoveStock(
            int reqNumber,
            int reqLine,
            string partNumber,
            decimal quantity,
            int? fromLocationId,
            int? fromPalletNumber,
            DateTime? fromStockDate,
            int? toLocationId,
            int? toPalletNumber,
            DateTime? toStockDate,
            string state,
            string stockPool)
        {
            var connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());

            var cmd = new OracleCommand("stores_pack.move_stock", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add(new OracleParameter("p_req_number", OracleDbType.Int32)
                                   {
                                       Direction = ParameterDirection.Input,
                                       Value = reqNumber
                                   });
            cmd.Parameters.Add(new OracleParameter("p_req_line", OracleDbType.Int32)
                                   {
                                       Direction = ParameterDirection.Input,
                                       Value = reqLine
                                   });
            cmd.Parameters.Add(new OracleParameter("p_part_number", OracleDbType.Varchar2)
                                   {
                                       Direction = ParameterDirection.Input,
                                       Value = partNumber
                                   });
            cmd.Parameters.Add(new OracleParameter("p_qty", OracleDbType.Decimal)
                                   {
                                       Direction = ParameterDirection.Input,
                                       Value = quantity
                                   });

            cmd.Parameters.Add(new OracleParameter("p_from_location_id", OracleDbType.Int32)
                                   {
                                       Direction = ParameterDirection.Input, Value = fromLocationId
                                   });
            cmd.Parameters.Add(new OracleParameter("p_from_pallet_number", OracleDbType.Int32)
                                   {
                                       Direction = ParameterDirection.Input, Value = fromPalletNumber
            });
            cmd.Parameters.Add(new OracleParameter("p_from_stock_date", OracleDbType.Date)
                                   {
                                       Direction = ParameterDirection.Input, Value = fromStockDate
            });
            cmd.Parameters.Add(new OracleParameter("p_to_location_id", OracleDbType.Int32)
                                   {
                                       Direction = ParameterDirection.Input,
                                       Value = toLocationId
            });
            cmd.Parameters.Add(new OracleParameter("p_to_pallet_number", OracleDbType.Int32)
                                   {
                                       Direction = ParameterDirection.Input,
                                       Value = toPalletNumber
            });
            cmd.Parameters.Add(new OracleParameter("p_new_stock_date", OracleDbType.Date)
                                   {
                                       Direction = ParameterDirection.Input,
                                       Value = toStockDate
            });
            cmd.Parameters.Add(new OracleParameter("p_stock_pool", OracleDbType.Varchar2)
                                   {
                                       Direction = ParameterDirection.Input,
                                       Value = stockPool
                                   });
            cmd.Parameters.Add(new OracleParameter("p_state", OracleDbType.Varchar2)
                                   {
                                       Direction = ParameterDirection.Input, Value = state
                                   });

            var messageParameter = new OracleParameter("p_message", OracleDbType.Varchar2)
                                   {
                                       Direction = ParameterDirection.Output, Size = 4000
                                   };
            cmd.Parameters.Add(messageParameter);

            var successParameter = new OracleParameter("p_success", OracleDbType.Int32)
                                       {
                                           Direction = ParameterDirection.Output
                                       };
            cmd.Parameters.Add(successParameter);

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

            return new ProcessResult
            {
                Message = messageParameter.Value.ToString(),
                Success = int.Parse(successParameter.Value.ToString()) == 1
            };
        }

        public int GetQuantityBookedIn(int purchaseOrderNumber, int line)
        {
            using (var connection = this.databaseService.GetConnection())
            {
                connection.Open();

                var cmd = new OracleCommand("stores_oo.qty_booked_in", connection)
                              {
                                  CommandType = CommandType.StoredProcedure
                              };

                var result = new OracleParameter("result", OracleDbType.Int32)
                                 {
                                     Direction = ParameterDirection.ReturnValue,
                                 };
                cmd.Parameters.Add(result);

                var arg1 = new OracleParameter("p_order_number", OracleDbType.Int32)
                               {
                                   Direction = ParameterDirection.Input,
                                   Value = purchaseOrderNumber
                               };
                cmd.Parameters.Add(arg1);

                var arg2 = new OracleParameter("p_order_line", OracleDbType.Int32)
                               {
                                   Direction = ParameterDirection.Input,
                                   Value = line
                               };
                cmd.Parameters.Add(arg2);

                cmd.ExecuteNonQuery();
                connection.Close();
                int.TryParse(result.Value.ToString(), out var res);
                return res;
            }
        }

        public bool ValidOrderQty(int orderNumber, int orderLine, int qty, out int qtyRec, out int ourQty)
        {
            using (var connection = this.databaseService.GetConnection())
            {
                connection.Open();
                var cmd = new OracleCommand("stores_oo.valid_order_qty_wrapper", connection)
                              {
                                  CommandType = CommandType.StoredProcedure
                              };

                var result = new OracleParameter("result", OracleDbType.Int32)
                                 {
                                     Direction = ParameterDirection.ReturnValue,
                                 };
                cmd.Parameters.Add(result);

                var arg1 = new OracleParameter("p_order_number", OracleDbType.Int32)
                               {
                                   Direction = ParameterDirection.Input,
                                   Value = orderNumber
                               };
                cmd.Parameters.Add(arg1);

                var arg2 = new OracleParameter("p_order_line", OracleDbType.Int32)
                               {
                                   Direction = ParameterDirection.Input,
                                   Value = orderLine
                               };
                cmd.Parameters.Add(arg2);

                var arg3 = new OracleParameter("p_qty", OracleDbType.Int32)
                               {
                                   Direction = ParameterDirection.Input,
                                   Value = qty
                               };
                cmd.Parameters.Add(arg3);

                var arg4 = new OracleParameter("p_qty_rec", OracleDbType.Int32)
                               {
                                   Direction = ParameterDirection.InputOutput,
                               };
                cmd.Parameters.Add(arg4);

                var arg5 = new OracleParameter("p_our_qty", OracleDbType.Int32)
                               {
                                   Direction = ParameterDirection.InputOutput,
                               };
                cmd.Parameters.Add(arg5);

                cmd.ExecuteNonQuery();
                connection.Close();

                int.TryParse(arg4.Value.ToString(), out qtyRec);
                int.TryParse(arg5.Value.ToString(), out ourQty);
                return int.Parse(result.Value.ToString()) == 0;
            }
        }

        public ProcessResult UnallocateReq(int reqNumber, int unallocatedBy)
        {
            using (var connection = this.databaseService.GetConnection())
            {
                connection.Open();
                var cmd = new OracleCommand("stores_oo.unpick_stock", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                var arg1 = new OracleParameter("p_req_number", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Input,
                    Value = reqNumber
                };
                cmd.Parameters.Add(arg1);

                var arg2 = new OracleParameter("p_line_number", OracleDbType.Int32)
                               {
                                   Direction = ParameterDirection.Input,
                               };
                cmd.Parameters.Add(arg2);

                var arg3 = new OracleParameter("p_unalloc_by", OracleDbType.Int32)
                               {
                                   Direction = ParameterDirection.Input,
                                   Value = unallocatedBy
                               };
                cmd.Parameters.Add(arg3);

                var arg4 = new OracleParameter("p_qty_to_allocate", OracleDbType.Int32)
                               {
                                   Direction = ParameterDirection.Input
                               };
                cmd.Parameters.Add(arg4);

                var arg5 = new OracleParameter("p_qty_allocated", OracleDbType.Int32)
                               {
                                   Direction = ParameterDirection.Input
                               };
                cmd.Parameters.Add(arg5);

                var arg6 = new OracleParameter("p_commit ", OracleDbType.Boolean)
                               {
                                   Direction = ParameterDirection.Input,
                                   Value = new OracleBoolean(true) 
                               };
                cmd.Parameters.Add(arg6);

                var successParameter = new OracleParameter("p_success ", OracleDbType.Int32)
                               {
                                   Direction = ParameterDirection.InputOutput,
                               };
                cmd.Parameters.Add(successParameter);

                var messageParam = new OracleParameter("p_message", OracleDbType.Varchar2)
                                       {
                                           Direction = ParameterDirection.Output,
                                       };
                cmd.Parameters.Add(messageParam);

                cmd.ExecuteNonQuery();
                connection.Close();

                if (int.TryParse(successParameter.Value.ToString(), out var success))
                {
                    if (success == 1)
                    {
                        return new ProcessResult(true, null);
                    }

                    return new ProcessResult(
                        false,
                        $"Failed to unallocate line: {reqNumber}. {messageParam.Value}");
                }

                return new ProcessResult(false, "Failed in procedure call: stores_oo.unallocate_req");
            }
        }

        public ProcessResult UnpickStock(
            int reqNumber,
            int lineNumber,
            int seq,
            int orderNumber,
            int orderLine,
            decimal qty,
            int? stockLocatorId,
            int amendedBy)
        {
            using (var connection = this.databaseService.GetConnection())
            {
                connection.Open();
                var cmd = new OracleCommand("stores_oo.unpick_stock_wrapper", connection)
                              {
                                  CommandType = CommandType.StoredProcedure
                              };

                var arg1 = new OracleParameter("p_req_number", OracleDbType.Int32)
                               {
                                   Direction = ParameterDirection.Input, Value = reqNumber
                               };
                cmd.Parameters.Add(arg1);

                var arg2 = new OracleParameter("p_line_number", OracleDbType.Int32)
                               {
                                   Direction = ParameterDirection.Input,
                               };
                cmd.Parameters.Add(arg2);

                var arg3 = new OracleParameter("p_seq", OracleDbType.Int32)
                               {
                                   Direction = ParameterDirection.Input, Value = seq
                               };
                cmd.Parameters.Add(arg3);

                var arg4 = new OracleParameter("p_order_number", OracleDbType.Int32)
                               {
                                   Direction = ParameterDirection.Input, Value = orderNumber
                               };
                cmd.Parameters.Add(arg4);

                var arg5 = new OracleParameter("p_order_line", OracleDbType.Int32)
                               {
                                   Direction = ParameterDirection.Input, Value = orderLine
                               };
                cmd.Parameters.Add(arg5);

                var arg6 = new OracleParameter("p_qty_to_unpick", OracleDbType.Decimal)
                               {
                                   Direction = ParameterDirection.Input, Value = qty
                               };
                cmd.Parameters.Add(arg6);

                var arg7 = new OracleParameter("p_stock_locator_id", OracleDbType.Int32)
                               {
                                   Direction = ParameterDirection.Input, Value = stockLocatorId
                               };
                cmd.Parameters.Add(arg7);

                var arg8 = new OracleParameter("p_upd_sod_alloc_qty", OracleDbType.Varchar2)
                               {
                                   Direction = ParameterDirection.Input, Value = "Y"
                               };
                cmd.Parameters.Add(arg8);

                var arg9 = new OracleParameter("p_amended_by", OracleDbType.Int32)
                               {
                                   Direction = ParameterDirection.Input, Value = amendedBy
                               };
                cmd.Parameters.Add(arg9);

                var successParameter = new OracleParameter("p_success", OracleDbType.Int32)
                                           {
                                               Direction = ParameterDirection.InputOutput,
                                               Value = 1
                                           };
                cmd.Parameters.Add(successParameter);

                var arg11 = new OracleParameter("p_realloc", OracleDbType.Varchar2)
                                {
                                    Direction = ParameterDirection.Input, Value = "N"
                                };
                cmd.Parameters.Add(arg11);

                var messageParam = new OracleParameter("p_message", OracleDbType.Varchar2)
                                       {
                                           Direction = ParameterDirection.InputOutput,
                                       };
                cmd.Parameters.Add(messageParam);

                cmd.ExecuteNonQuery();
                connection.Close();

                if (int.TryParse(successParameter.Value.ToString(), out var success))
                {
                    if (success == 1)
                    {
                        return new ProcessResult(true, null);
                    }

                    return new ProcessResult(
                        false,
                        $"Failed to unallocate line: {reqNumber},{lineNumber},{seq}. {messageParam.Value}");
                }

                return new ProcessResult(false, "Failed in procedure call: stores_oo.unpick_stock");
            }
        }
    }
}
