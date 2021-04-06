namespace Linn.Stores.Proxy
{
    using System;
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
                var cmd = new OracleCommand("EXPORT_RETURN_PACK.MAKE_EXPORT_RETURN", connection)
                              {
                                  CommandType = CommandType.StoredProcedure
                              };

                var result = new OracleParameter(null, OracleDbType.Int32)
                                 {
                                     Direction = ParameterDirection.ReturnValue
                                 };
                cmd.Parameters.Add(result);

                var rsnsParameter = new OracleParameter("p_rsns", OracleDbType.Varchar2)
                                        {
                                            Direction = ParameterDirection.Input, Value = rsns, Size = 2000
                                        };
                cmd.Parameters.Add(rsnsParameter);

                var hubParam = new OracleParameter("p_hub_return", OracleDbType.Varchar2)
                                   {
                                       Direction = ParameterDirection.Input, Value = hubReturn
                                   };
                cmd.Parameters.Add(hubParam);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                return int.Parse(result.Value.ToString());
            }
        }
    }
}