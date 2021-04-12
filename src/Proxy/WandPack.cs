namespace Linn.Stores.Proxy
{
    using System.Data;

    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Wand.Models;

    using Oracle.ManagedDataAccess.Client;

    public class WandPack : IWandPack
    {
        public WandPackResult Wand(string transType, int userNumber, int consignmentId, string wandString)
        {
            var connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());

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
                        Value = 1
                    });
            var wandLogParameter = new OracleParameter("p_wandlog_id", OracleDbType.Int32)
                                       {
                                           Direction = ParameterDirection.Output,
                                           Size = 20
                                       };
            cmd.Parameters.Add(wandLogParameter);

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

            var success = int.Parse(successParameter.Value.ToString()) == 1;
            var gotWandLog = int.TryParse(wandLogParameter.Value.ToString(), out var wandLogId);
            return new WandPackResult
            {
                           Message = messageParameter.Value.ToString(),
                           Success = success,
                           WandLogId = gotWandLog ? wandLogId : (int?)null
                       };
        }
    }
}
