namespace Linn.Stores.Proxy
{
    using System.Data;

    using Linn.Stores.Domain.LinnApps.ExternalServices;

    using Oracle.ManagedDataAccess.Client;

    public class WorkstationPackProxy : IWorkstationPack
    {
        private readonly IDatabaseService databaseService;

        public WorkstationPackProxy(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public void StartTopUpRun()
        {
            using (var connection = this.databaseService.GetConnection())
            {
                connection.Open();
                var cmd = new OracleCommand("workstation_pack.start_top_up_run", connection)
                              {
                                  CommandType = CommandType.StoredProcedure
                              };

                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public string TopUpRunProgressStatus()
        {
            using (var connection = this.databaseService.GetConnection())
            {
                connection.Open();
                var cmd = new OracleCommand("workstation_pack.top_up_run_progress_status", connection)
                              {
                                  CommandType = CommandType.StoredProcedure
                              };

                var result = new OracleParameter(null, OracleDbType.Varchar2)
                                 {
                                     Direction = ParameterDirection.ReturnValue,
                                     Size = 100
                                 };
                cmd.Parameters.Add(result);

                cmd.ExecuteNonQuery();
                connection.Close();
                return result.Value?.ToString() == "null" ? null : result.Value?.ToString();
            }
        }
    }
}
