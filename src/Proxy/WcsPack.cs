namespace Linn.Stores.Proxy
{
    using System;
    using System.Data;

    using Linn.Common.Proxy.LinnApps;
    using Linn.Stores.Domain.LinnApps.ExternalServices;

    using Oracle.ManagedDataAccess.Client;

    public class WcsPack : IWcsPack
    {
        public void MoveDpqPalletsToUpper()
        {
            var connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());

            var cmd = new OracleCommand("WCS_PACK.MOVE_DPQ_PALLETS_TO_UPPER", connection)
                          {
                              CommandType = CommandType.StoredProcedure
                          };

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public bool CanMovePalletToUpper(int palletNumber)
        {
            var connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());

            var cmd = new OracleCommand("WCS_PACK.can_move_pallet_to_upper_sql", connection)
                          {
                              CommandType = CommandType.StoredProcedure
                          };

            var result = new OracleParameter(null, OracleDbType.Int32) {Direction = ParameterDirection.ReturnValue};
            cmd.Parameters.Add(result);

            cmd.Parameters.Add(
                new OracleParameter("p_pallet_id", OracleDbType.Int32)
                    {
                        Direction = ParameterDirection.Input, Value = palletNumber
                    });

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

            return int.Parse(result.Value.ToString()) == 1;
        }

        public void MovePalletToUpper(int palletNumber, string reference)
        {
            var connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());

            var cmd = new OracleCommand("WCS_PACK.MOVE_PALLET_TO_UPPER", connection)
                          {
                              CommandType = CommandType.StoredProcedure
                          };

            cmd.Parameters.Add(
                new OracleParameter("p_pallet_id", OracleDbType.Int32)
                    {
                        Direction = ParameterDirection.Input, Value = palletNumber
                    });
            cmd.Parameters.Add(
                new OracleParameter("p_collective_ref", OracleDbType.Varchar2)
                    {
                        Direction = ParameterDirection.Input, Size = 50, Value = reference
                    });

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public int NextTaskSeq()
        {
            var connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());

            var cmd = new OracleCommand("WCS_PACK.NEXT_TASK_SEQ", connection)
                          {
                              CommandType = CommandType.StoredProcedure
                          };

            var result = new OracleParameter(null, OracleDbType.Int32)
                             {
                                 Direction = ParameterDirection.ReturnValue, Size = 50
                             };
            cmd.Parameters.Add(result);

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

            return Convert.ToInt32(result.Value.ToString());
        }

        public string PalletLocation(int palletNumber)
        {
            var connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());

            var cmd = new OracleCommand("WCS_PACK.PALLET_LOCATION", connection)
                          {
                              CommandType = CommandType.StoredProcedure
                          };

            var result = new OracleParameter(null, OracleDbType.Varchar2)
                             {
                                 Direction = ParameterDirection.ReturnValue, Size = 50
                             };
            cmd.Parameters.Add(result);

            cmd.Parameters.Add(
                new OracleParameter("p_pallet_id", OracleDbType.Int32)
                    {
                        Direction = ParameterDirection.Input, Value = palletNumber
                    });

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

            return result.Value.ToString();
        }

        public int? PalletAtLocation(string location)
        {
            var connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());

            var cmd = new OracleCommand("WCS_PACK.PALLET_AT_LOCATION", connection)
                          {
                              CommandType = CommandType.StoredProcedure
                          };

            var result = new OracleParameter(null, OracleDbType.Int32)
                             {
                                 Direction = ParameterDirection.ReturnValue,
                                 Size = 50
                             };
            cmd.Parameters.Add(result);

            cmd.Parameters.Add(
                new OracleParameter("p_pallet_id", OracleDbType.Varchar2)
                    {
                        Direction = ParameterDirection.Input,
                        Size = 50,
                        Value = location
                });

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

            var palletId = result.Value.ToString();
            if (string.IsNullOrEmpty(palletId) || palletId == "null")
            {
                return null;
            }

            return Convert.ToInt32(palletId);
        }

        public int MovePallet(int palletNumber, string destination, int priority, string taskSource, int who)
        {
            var connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());

            var cmd = new OracleCommand("WCS_PACK.MOVE_PALLET", connection)
                          {
                              CommandType = CommandType.StoredProcedure
                          };

            var result = new OracleParameter(null, OracleDbType.Int32)
                             {
                                 Direction = ParameterDirection.ReturnValue,
                                 Size = 50
                             };
            cmd.Parameters.Add(result);

            cmd.Parameters.Add(
                new OracleParameter("p_pallet_id", OracleDbType.Int32)
                    {
                        Direction = ParameterDirection.Input,
                        Value = palletNumber
                    });

            cmd.Parameters.Add(
                new OracleParameter("p_destination", OracleDbType.Varchar2)
                    {
                        Direction = ParameterDirection.Input,
                        Size = 50,
                        Value = destination
                });

            cmd.Parameters.Add(
                new OracleParameter("p_priority", OracleDbType.Int32)
                    {
                        Direction = ParameterDirection.Input,
                        Value = priority
                });

            cmd.Parameters.Add(
                new OracleParameter("p_task_source", OracleDbType.Varchar2)
                    {
                        Direction = ParameterDirection.Input,
                        Size = 50,
                        Value = taskSource
                });

            cmd.Parameters.Add(
                new OracleParameter("p_who", OracleDbType.Int32)
                    {
                        Direction = ParameterDirection.Input,
                        Value = who
                    });

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

            return Convert.ToInt32(result.Value.ToString());
        }

        public int AtMovePallet(int palletNumber, string fromLocation, string destination, int priority, string taskSource, int who)
        {
            var connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());

            var cmd = new OracleCommand("WCS_PACK.ATMOVE_PALLET", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            var result = new OracleParameter(null, OracleDbType.Int32)
            {
                Direction = ParameterDirection.ReturnValue,
                Size = 50
            };
            cmd.Parameters.Add(result);

            cmd.Parameters.Add(
                new OracleParameter("p_pallet_id", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Input,
                    Value = palletNumber
                });

            cmd.Parameters.Add(
                new OracleParameter("p_from_location", OracleDbType.Varchar2)
                    {
                        Direction = ParameterDirection.Input,
                        Size = 50,
                        Value = fromLocation
                    });

            cmd.Parameters.Add(
                new OracleParameter("p_destination", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Size = 50,
                    Value = destination
                });

            cmd.Parameters.Add(
                new OracleParameter("p_priority", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Input,
                    Value = priority
                });

            cmd.Parameters.Add(
                new OracleParameter("p_task_source", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Size = 50,
                    Value = taskSource
                });

            cmd.Parameters.Add(
                new OracleParameter("p_who", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Input,
                    Value = who
                });

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

            return Convert.ToInt32(result.Value.ToString());
        }

        public int EmptyLocation(int palletNumber, string location, int priority, string taskSource, int who)
        {
            var connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());

            var cmd = new OracleCommand("WCS_PACK.EMPTY_LOCATION", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            var result = new OracleParameter(null, OracleDbType.Int32)
            {
                Direction = ParameterDirection.ReturnValue,
                Size = 50
            };
            cmd.Parameters.Add(result);

            cmd.Parameters.Add(
                new OracleParameter("p_pallet_id", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Input,
                    Value = palletNumber
                });

            cmd.Parameters.Add(
                new OracleParameter("p_location", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Size = 50,
                    Value = location
                });

            cmd.Parameters.Add(
                new OracleParameter("p_priority", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Input,
                    Value = priority
                });

            cmd.Parameters.Add(
                new OracleParameter("p_task_source", OracleDbType.Varchar2)
                {
                    Direction = ParameterDirection.Input,
                    Size = 50,
                    Value = taskSource
                });

            cmd.Parameters.Add(
                new OracleParameter("p_who", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Input,
                    Value = who
                });

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

            return Convert.ToInt32(result.Value.ToString());
        }
    }
}
