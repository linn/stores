namespace Linn.Stores.Proxy
{
    using System.Data;

    using Linn.Stores.Domain.LinnApps.ExternalServices;

    using Oracle.ManagedDataAccess.Client;

    public class ExportReturnPack : IExportReturnPack
    {
        public int MakeExportReturn(string rsns, bool hubReturn)
        {
            var connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());

            var cmd = new OracleCommand("EXPORT_RETURN_PACK.MAKE_EXPORT_RETURN", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            var rsnsParameter = new OracleParameter("p_rsns", OracleDbType.Varchar2)
            {
                Direction = ParameterDirection.Input,
                Size = 200,
                Value = rsns
            };
            cmd.Parameters.Add(rsnsParameter);

            var pHubReturn = new OracleParameter("p_hub_return", OracleDbType.Varchar2)
            {
                Direction = ParameterDirection.Input,
                Size = 1,
                Value = hubReturn ? "y" : "n"
            };
            cmd.Parameters.Add(pHubReturn);

            var result = new OracleParameter(null, OracleDbType.Int32)
            {
                Direction = ParameterDirection.ReturnValue
            };
            cmd.Parameters.Add(result);

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
            return int.Parse(result.Value.ToString());
        }
    }
}