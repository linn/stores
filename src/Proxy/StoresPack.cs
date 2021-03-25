namespace Linn.Stores.Proxy
{
    using System;
    using System.Data;

    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Domain.LinnApps.StockMove.Models;

    using Oracle.ManagedDataAccess.Client;

    public class StoresPack : IStoresPack
    {
        public ProcessResult UnAllocateRequisition(int reqNumber, int? reqLineNumber, int userNumber)
        {
            var connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());

            var cmd = new OracleCommand("stores_pack.unalloc_req", connection)
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

            return new ProcessResult
            {
                Message = messageParameter.Value.ToString(),
                Success = int.Parse(successParameter.Value.ToString()) == 1
            };
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
            int quantity,
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
            cmd.Parameters.Add(new OracleParameter("p_qty", OracleDbType.Int32)
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
            int quantity,
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
            cmd.Parameters.Add(new OracleParameter("p_qty", OracleDbType.Int32)
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
    }
}
