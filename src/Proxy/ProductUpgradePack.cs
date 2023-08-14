namespace Linn.Stores.Proxy
{
    using System.Data;

    using Linn.Common.Proxy.LinnApps;
    using Linn.Stores.Domain.LinnApps.ExternalServices;

    using Oracle.ManagedDataAccess.Client;

    public class ProductUpgradePack : IProductUpgradePack
    {
        private readonly IDatabaseService databaseService;

        public ProductUpgradePack(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public int GetRenewSernosFromOriginal(int serialNumber)
        {
            using (var connection = this.databaseService.GetConnection())
            {
                connection.Open();
                var cmd = new OracleCommand("product_upgrade_pack.get_renew_sernos_from_original", connection)
                              {
                                  CommandType = CommandType.StoredProcedure
                              };

                var result = new OracleParameter(null, OracleDbType.Int32)
                                 {
                                     Direction = ParameterDirection.ReturnValue
                                 };
                cmd.Parameters.Add(result);

                var arg = new OracleParameter("p_sernos", OracleDbType.Int32)
                              {
                                  Direction = ParameterDirection.Input,
                                  Value = serialNumber
                              };
                cmd.Parameters.Add(arg);

                cmd.ExecuteNonQuery();
                connection.Close();
                return int.Parse(result.Value.ToString());
            }
        }
    }
}
