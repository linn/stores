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
    }
}
