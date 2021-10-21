namespace Linn.Stores.Proxy
{
    using System.Data;

    using Linn.Common.Proxy.LinnApps;
    using Linn.Stores.Domain.LinnApps.ExternalServices;

    using Oracle.ManagedDataAccess.Client;

    public class PurchaseLedgerPack : IPurchaseLedgerPack
    {
        private readonly IDatabaseService databaseService;

        public PurchaseLedgerPack(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public int GetLedgerPeriod()
        {
            using (var connection = this.databaseService.GetConnection())
            {
                connection.Open();
                var cmd = new OracleCommand("pl_pack.get_pl_ledger_period", connection)
                              {
                                  CommandType = CommandType.StoredProcedure
                              };
                var result = new OracleParameter("document_type", OracleDbType.Varchar2)
                                 {
                                     Direction = ParameterDirection.ReturnValue, Size = 50
                                 };
                cmd.Parameters.Add(result);

                cmd.ExecuteNonQuery();
                connection.Close();
                var res = result.Value.ToString();
                return int.Parse(res);
            }
        }

        public int GetNextLedgerSeq()
        {
            return this.databaseService.GetNextVal("PL_LEDGER_SEQ");
        }

        public int GetNomacc(string department, string nom)
        {
            using (var connection = this.databaseService.GetConnection())
            {
                connection.Open();
                var cmd = new OracleCommand("pl_pack.get_nomacc", connection)
                              {
                                  CommandType = CommandType.StoredProcedure
                              };

                var result = new OracleParameter(null, OracleDbType.Int32)
                                 {
                                     Direction = ParameterDirection.ReturnValue, Size = 50
                                 };
                cmd.Parameters.Add(result);

                var parameter = new OracleParameter("p_dept", OracleDbType.Varchar2)
                                    {
                                        Direction = ParameterDirection.Input, Value = department
                                    };
                cmd.Parameters.Add(parameter);

                var num = new OracleParameter("p_nom", OracleDbType.Varchar2)
                              {
                                  Direction = ParameterDirection.Input, Value = nom
                              };
                cmd.Parameters.Add(num);

                cmd.ExecuteNonQuery();
                connection.Close();
                var res = result.Value.ToString();
                return int.Parse(res);
            }
        }
    }
}
