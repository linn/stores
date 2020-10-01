namespace Linn.Stores.Proxy
{
    using System.Data;

    using Linn.Stores.Domain.LinnApps.ExternalServices;

    using Oracle.ManagedDataAccess.Client;

    public class PartPack : IPartPack
    {
        private readonly IDatabaseService databaseService;

        public PartPack(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public string PartRoot(string partNumber)
        {
            using (var connection = this.databaseService.GetConnection())
            {
                connection.Open();
                var cmd = new OracleCommand("part_pack.part_root", connection) { CommandType = CommandType.StoredProcedure };

                var result = new OracleParameter(null, OracleDbType.Varchar2)
                                 {
                                     Direction = ParameterDirection.ReturnValue,
                                     Size = 50
                                 };
                cmd.Parameters.Add(result);

                var arg = new OracleParameter("p_part_number", OracleDbType.Varchar2)
                                 {
                                     Direction = ParameterDirection.Input,
                                     Size = 14,
                                     Value = partNumber
                                 };
                cmd.Parameters.Add(arg);
               

                cmd.ExecuteNonQuery();
                connection.Close();
                return result.Value.ToString();
            }
        }
    }
}