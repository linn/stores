namespace Linn.Stores.Proxy
{
    using System.Data;

    using Linn.Stores.Domain.LinnApps.ExternalServices;

    using Oracle.ManagedDataAccess.Client;

    public class SosPack : ISosPack
    {
        private readonly IDatabaseService databaseService;

        public SosPack(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public void SetNewJobId()
        {
            using (var connection = this.databaseService.GetConnection())
            {
                connection.Open();
                var cmd = new OracleCommand("sos.set_new_job_id", connection)
                              {
                                  CommandType = CommandType.StoredProcedure
                              };

                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public int GetJobId()
        {
            using (var connection = this.databaseService.GetConnection())
            {
                connection.Open();
                var cmd = new OracleCommand("sos.job_id", connection)
                              {
                                  CommandType = CommandType.StoredProcedure
                              };

                var result = new OracleParameter(null, OracleDbType.Int32)
                                 {
                                     Direction = ParameterDirection.ReturnValue
                                 };
                cmd.Parameters.Add(result);

                cmd.ExecuteNonQuery();
                connection.Close();
                return int.Parse(result.Value.ToString());
            }
        }
    }
}