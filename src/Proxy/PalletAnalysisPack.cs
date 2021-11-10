namespace Linn.Stores.Proxy
{
    using System.Data;

    using Linn.Common.Proxy.LinnApps;
    using Linn.Stores.Domain.LinnApps.ExternalServices;

    using Oracle.ManagedDataAccess.Client;

    public class PalletAnalysisPack : IPalletAnalysisPack
    {
        private readonly IDatabaseService databaseService;

        public PalletAnalysisPack(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public bool CanPutPartOnPallet(string partNumber, string palletNumber)
        {
            using (var connection = this.databaseService.GetConnection())
            {
                connection.Open();
                var cmd = new OracleCommand("pallet_analysis_pack.can_put_part_on_pallet_wrapper", connection)
                              {
                                  CommandType = CommandType.StoredProcedure
                              };

                var result = new OracleParameter(null, OracleDbType.Int32)
                                 {
                                     Direction = ParameterDirection.ReturnValue,
                                 };
                cmd.Parameters.Add(result);

                var arg1 = new OracleParameter("p_part_number", OracleDbType.Varchar2)
                              {
                                  Direction = ParameterDirection.Input,
                                  Size = 14,
                                  Value = partNumber
                              };
                cmd.Parameters.Add(arg1);

                var arg2 = new OracleParameter("p_pallet_number", OracleDbType.Varchar2)
                               {
                                   Direction = ParameterDirection.Input,
                                   Size = 50,
                                   Value = palletNumber
                               };
                cmd.Parameters.Add(arg2);

                cmd.ExecuteNonQuery();
                connection.Close();
                return int.Parse(result.Value.ToString()) == 0;
            }
        }

        public string Message()
        {
            using (var connection = this.databaseService.GetConnection())
            {
                connection.Open();
                var cmd = new OracleCommand("pallet_analysis_pack.message", connection)
                              {
                                  CommandType = CommandType.StoredProcedure
                              };

                var result = new OracleParameter(null, OracleDbType.Varchar2)
                                 {
                                     Direction = ParameterDirection.ReturnValue,
                                     Size = 500
                                 };
                cmd.Parameters.Add(result);

                cmd.ExecuteNonQuery();
                connection.Close();
                return result.Value.ToString();
            }
        }
    }
}
