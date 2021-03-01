namespace Linn.Stores.Proxy
{
    using System.Data;

    using Linn.Stores.Domain.LinnApps.ExternalServices;

    using Oracle.ManagedDataAccess.Client;

    public class ExportReturnsPack : IExportReturnsPack
    {
        private readonly IDatabaseService databaseService;

        public ExportReturnsPack(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public int MakeExportReturn(string rsns, string hubReturn)
        {
            using (var connection = this.databaseService.GetConnection())
            {
                connection.Open();
                
                var cmd = new OracleCommand("EXPORT_RETURN_PACK.MAKE_EXPORT_RETURN", connection)
                              {
                                  CommandType = CommandType.StoredProcedure
                              };

                cmd.Parameters.Add(
                    new OracleParameter("p_rsns", OracleDbType.Varchar2)
                        {
                            Direction = ParameterDirection.Input, Value = rsns, Size = 2000
                        });

                cmd.Parameters.Add(
                    new OracleParameter("p_hub_return", OracleDbType.Varchar2)
                        {
                            Direction = ParameterDirection.Input, Value = hubReturn
                        });

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