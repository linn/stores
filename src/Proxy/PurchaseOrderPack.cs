namespace Linn.Stores.Proxy
{
    using System.Data;

    using Linn.Common.Proxy.LinnApps;
    using Linn.Stores.Domain.LinnApps.ExternalServices;

    using Oracle.ManagedDataAccess.Client;

    public class PurchaseOrderPack : IPurchaseOrderPack
    {
        private readonly IDatabaseService databaseService;

        public PurchaseOrderPack(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public string GetDocumentType(int orderNumber)
        {
            using (var connection = this.databaseService.GetConnection())
            {
                connection.Open();
                var cmd =
                    new OracleCommand(
                        "pl_orders_pack.get_document_type",
                        connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                var result = new OracleParameter("document_type", OracleDbType.Varchar2)
                                 {
                                     Direction = ParameterDirection.ReturnValue,
                                     Size = 50
                                 };
                cmd.Parameters.Add(result);

                var orderNumberParam = new OracleParameter("p_order_number", OracleDbType.Int32)
                                         {
                                             Direction = ParameterDirection.Input,
                                             Value = orderNumber
                                         };
                cmd.Parameters.Add(orderNumberParam);
                cmd.ExecuteNonQuery();
                connection.Close();
                return result.Value.ToString();
            }
        }
    }
}
