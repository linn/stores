namespace Linn.Stores.Proxy
{
    using System.Data;

    using Linn.Common.Proxy.LinnApps;
    using Linn.Stores.Domain.LinnApps.ExternalServices;

    using Oracle.ManagedDataAccess.Client;

    public class WwdPack : IWwdPack
    {
        public void WWD(string partNumber, string workStationCode, int quantity)
        {
            var connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());

            var cmd = new OracleCommand("WWD_PACK.WWD", connection) { CommandType = CommandType.StoredProcedure };

            var partNumberParameter = new OracleParameter("p_assembly_number", OracleDbType.Varchar2)
                                          {
                                              Direction = ParameterDirection.Input,
                                              Size = 50,
                                              Value = partNumber
                                          };
            cmd.Parameters.Add(partNumberParameter);

            var workStationCodeParameter = new OracleParameter("p_work_station_code", OracleDbType.Varchar2)
                                               {
                                                   Direction = ParameterDirection.Input,
                                                   Size = 16,
                                                   Value = workStationCode
                                               };
            cmd.Parameters.Add(workStationCodeParameter);

            var quantityParameter = new OracleParameter("p_qty", OracleDbType.Int32)
                                        {
                                            Direction = ParameterDirection.Input, Value = quantity
                                        };
            cmd.Parameters.Add(quantityParameter);

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public int JobId()
        {
            var connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());

            var cmd = new OracleCommand("WWD_PACK.JOB_ID", connection) { CommandType = CommandType.StoredProcedure };

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
