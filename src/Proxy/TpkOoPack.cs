namespace Linn.Stores.Proxy
{
    using System.Data;

    using Linn.Stores.Domain.LinnApps.ExternalServices;

    using Oracle.ManagedDataAccess.Client;

    public class TpkOoPack : ITpkOoPack
    {
        private readonly IDatabaseService databaseService;

        public TpkOoPack(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public string GetTpkNotes(int consignmentId, string fromLocation)
        {
            using (var connection = this.databaseService.GetConnection())
            {
                connection.Open();
                var cmd = new OracleCommand("tpk_oo.wtw_type", connection) { CommandType = CommandType.StoredProcedure };

                var result = new OracleParameter(null, OracleDbType.Varchar2)
                                 {
                                     Direction = ParameterDirection.ReturnValue,
                                     Size = 50
                                 };
                cmd.Parameters.Add(result);

                var arg1 = new OracleParameter("p_consignment_id", OracleDbType.Varchar2)
                              {
                                  Direction = ParameterDirection.Input,
                                  Size = 50,
                                  Value = consignmentId
                              };
                cmd.Parameters.Add(arg1);

                var arg2 = new OracleParameter("p_storage_place", OracleDbType.Varchar2)
                              {
                                  Direction = ParameterDirection.Input,
                                  Size = 50,
                                  Value = consignmentId
                              };
                cmd.Parameters.Add(arg2);

                cmd.ExecuteNonQuery();
                connection.Close();
                return result.Value.ToString();
            }
        }
    }
}
