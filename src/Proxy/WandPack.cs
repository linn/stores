namespace Linn.Stores.Proxy
{
    using System.Data;

    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Wand.Models;

    using Oracle.ManagedDataAccess.Client;

    public class WandPack : IWandPack
    {
        public WandResult Wand(string transType, int userNumber, int consignmentId, string wandString)
        {
            var connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());
            var wandResult = new WandResult();
            var success = 0;

            var cmd = new OracleCommand("wand_pack.wand_remote", connection)
                          {
                              CommandType = CommandType.StoredProcedure
                          };
            cmd.Parameters.Add(
                new OracleParameter("p_trans_type", OracleDbType.Varchar2)
                    {
                        Direction = ParameterDirection.Input, Value = transType
                    });
            cmd.Parameters.Add(
                new OracleParameter("p_user_number", OracleDbType.Int32)
                    {
                        Direction = ParameterDirection.Input,
                        Value = userNumber
                    });
            cmd.Parameters.Add(
                new OracleParameter("p_consignment_id", OracleDbType.Int32)
                    {
                        Direction = ParameterDirection.Input,
                        Value = consignmentId
                    });
            cmd.Parameters.Add(
                new OracleParameter("p_wand_string", OracleDbType.Varchar2)
                    {
                        Direction = ParameterDirection.Input,
                        Value = wandString
                    });
            cmd.Parameters.Add(
                new OracleParameter("p_do_updates", OracleDbType.Int32)
                    {
                        Direction = ParameterDirection.Input,
                        Value = 0
                    });
            cmd.Parameters.Add(
                new OracleParameter("p_message", OracleDbType.Varchar2)
                    {
                        Direction = ParameterDirection.Output,
                        Value = wandResult.Message
                    });
            cmd.Parameters.Add(
                new OracleParameter("p_success", OracleDbType.Int32)
                    {
                        Direction = ParameterDirection.Output,
                        Value = success
                    });

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

            return wandResult;
        }
    }
}
