namespace Linn.Stores.Proxy
{
    using System;
    using System.Data;

    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Models;

    using Oracle.ManagedDataAccess.Client;

    public class KardexPack : IKardexPack
    {
        public ProcessResult MoveStockFromKardex(
            int reqNumber,
            int reqLine,
            string kardexLocation,
            string partNumber,
            decimal quantity,
            string storageType,
            int? toLocationId,
            int? toPalletNumber)
        {
            var connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());

            var cmd = new OracleCommand("kardex_pack.move_stock_from_kardex", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add(new OracleParameter("p_req_number", OracleDbType.Int32)
                                   {
                                       Direction = ParameterDirection.Input, Value = reqNumber
                                   });
            cmd.Parameters.Add(new OracleParameter("p_req_line", OracleDbType.Int32)
                                   {
                                       Direction = ParameterDirection.Input, Value = reqLine
                                   });
            cmd.Parameters.Add(new OracleParameter("p_kardex_location", OracleDbType.Varchar2)
                                   {
                                       Direction = ParameterDirection.Input, Value = kardexLocation
                                   });
            cmd.Parameters.Add(new OracleParameter("p_part_number", OracleDbType.Varchar2)
                                   {
                                       Direction = ParameterDirection.Input,
                                       Value = partNumber
                                   });
            cmd.Parameters.Add(new OracleParameter("p_qty", OracleDbType.Decimal)
                                   {
                                       Direction = ParameterDirection.Input, Value = quantity
                                   });

            cmd.Parameters.Add(new OracleParameter("p_storage_type", OracleDbType.Varchar2)
                                   {
                                       Direction = ParameterDirection.Input, Value = storageType
                                   });
            cmd.Parameters.Add(new OracleParameter("p_to_storage_location_id", OracleDbType.Int32)
                                   {
                                       Direction = ParameterDirection.Input, Value = toLocationId
                                   });
            cmd.Parameters.Add(new OracleParameter("p_to_pallet_number", OracleDbType.Int32)
                                   {
                                       Direction = ParameterDirection.Input, Value = toPalletNumber
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

        public ProcessResult MoveStockToKardex(
            int reqNumber,
            int reqLine,
            string kardexLocation,
            string partNumber,
            decimal quantity,
            DateTime? fromStockDate,
            string storageType,
            int? fromLocationId,
            int? fromPalletNumber,
            DateTime? toStockDate,
            string locationFlag)
        {
            var connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());

            var cmd = new OracleCommand("kardex_pack.move_stock_to_kardex", connection)
                          {
                              CommandType = CommandType.StoredProcedure
                          };

            cmd.Parameters.Add(new OracleParameter("p_req_number", OracleDbType.Int32)
                                   {
                                       Direction = ParameterDirection.Input, Value = reqNumber
                                   });
            cmd.Parameters.Add(new OracleParameter("p_req_line", OracleDbType.Int32)
                                   {
                                       Direction = ParameterDirection.Input, Value = reqLine
                                   });
            cmd.Parameters.Add(new OracleParameter("p_kardex_location", OracleDbType.Varchar2)
                                   {
                                       Direction = ParameterDirection.Input, Value = kardexLocation
                                   });
            cmd.Parameters.Add(new OracleParameter("p_qty", OracleDbType.Decimal)
                                   {
                                       Direction = ParameterDirection.Input,
                                       Value = quantity
                                   });
            cmd.Parameters.Add(new OracleParameter("p_from_stock_rotation_date", OracleDbType.Date)
                                   {
                                       Direction = ParameterDirection.Input,
                                       Value = fromStockDate
                                   });
            cmd.Parameters.Add(new OracleParameter("p_storage_type", OracleDbType.Varchar2)
                                   {
                                       Direction = ParameterDirection.Input,
                                       Value = storageType
                                   });
            cmd.Parameters.Add(new OracleParameter("p_part_number", OracleDbType.Varchar2)
                                   {
                                       Direction = ParameterDirection.Input, Value = partNumber
                                   });
            cmd.Parameters.Add(new OracleParameter("p_from_storage_location_id", OracleDbType.Int32)
                                   {
                                       Direction = ParameterDirection.Input, Value = fromLocationId
                                   });
            cmd.Parameters.Add(new OracleParameter("p_from_pallet_number", OracleDbType.Int32)
                                   {
                                       Direction = ParameterDirection.Input, Value = fromPalletNumber
                                   });
            cmd.Parameters.Add(new OracleParameter("p_new_stock_rotation_date", OracleDbType.Date)
                                   {
                                       Direction = ParameterDirection.Input,
                                       Value = toStockDate
                                   });
            cmd.Parameters.Add(new OracleParameter("p_loc_proc_flag", OracleDbType.Varchar2)
                                   {
                                       Direction = ParameterDirection.Input,
                                       Value = locationFlag
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
