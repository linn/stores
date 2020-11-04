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

        public bool PartLiveTest(string partNumber, out string message)
        {
            using (var connection = this.databaseService.GetConnection())
            {
                connection.Open();
                var cmd = new OracleCommand("part_pack.part_live_test_sql", connection)
                              {
                                  CommandType = CommandType.StoredProcedure
                              };

                var result = new OracleParameter(null, OracleDbType.Int32)
                                 {
                                     Direction = ParameterDirection.ReturnValue, Size = 50
                                 };
                cmd.Parameters.Add(result);

                var arg = new OracleParameter("part_number", OracleDbType.Varchar2)
                              {
                                  Direction = ParameterDirection.Input, Size = 14, Value = partNumber
                              };
                cmd.Parameters.Add(arg);

                var msg = new OracleParameter("p_working", OracleDbType.Varchar2)
                              {
                                  Direction = ParameterDirection.InputOutput, Size = 100
                              };
                cmd.Parameters.Add(msg);


                cmd.ExecuteNonQuery();
                message = cmd.Parameters[2].Value.ToString();
                connection.Close();
                return int.Parse(result.Value.ToString()) == 1;
            }
        }
    }
}
