namespace Linn.Stores.Proxy
{
    using System.Data;

    using Linn.Stores.Domain.LinnApps.ExternalServices;

    using Oracle.ManagedDataAccess.Client;

    public class BundleLabelPack : IBundleLabelPack
    {
        private readonly IDatabaseService databaseService;

        public BundleLabelPack(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public void PrintTpkBoxLabels(string fromLocation)
        {
            using (var connection = this.databaseService.GetConnection())
            {
                connection.Open();
                var cmd = new OracleCommand("bundle_label_pack.print_tpk_box_labels ", connection)
                              {
                                  CommandType = CommandType.StoredProcedure
                              };
                var arg1 = new OracleParameter("p_storage_place", OracleDbType.Varchar2)
                               {
                                   Direction = ParameterDirection.Input,
                                   Size = 50,
                                   Value = fromLocation
                               };
                cmd.Parameters.Add(arg1);

                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
