namespace Linn.Stores.Proxy
{
    using System.Data;

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

            var result = new OracleParameter(null, OracleDbType.Int32)
                             {
                                 Direction = ParameterDirection.ReturnValue
                             };
            cmd.Parameters.Add(result);

            cmd.Parameters.Add(new OracleParameter("p_pallet_id", OracleDbType.Int32)
                                   {
                                       Direction = ParameterDirection.Input,
                                       Value = palletNumber
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

            cmd.Parameters.Add(new OracleParameter("p_pallet_id", OracleDbType.Int32)
                                   {
                                       Direction = ParameterDirection.Input,
                                       Value = palletNumber
                                   });
            cmd.Parameters.Add(new OracleParameter("p_collective_ref", OracleDbType.Varchar2)
                                   {
                                       Direction = ParameterDirection.Input,
                                       Size = 50,
                                       Value = reference
                                   });

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
}
