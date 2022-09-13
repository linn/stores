namespace Linn.Stores.Proxy
{
    using System;
    using System.Data;

    using Linn.Common.Proxy.LinnApps;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Models;

    using Oracle.ManagedDataAccess.Client;

    public class InvoicingPack : IInvoicingPack
    {
        private readonly IDatabaseService databaseService;

        public InvoicingPack(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public ProcessResult InvoiceConsignment(int consignmentId, int closedById)
        {
            using (var connection = this.databaseService.GetConnection())
            {
                var cmd = new OracleCommand("invoicing_oo.make_invoices_for_consignment", connection)
                              {
                                  CommandType = CommandType.StoredProcedure
                              };

                cmd.Parameters.Add(
                    new OracleParameter("p_consignment_id", OracleDbType.Int32)
                        {
                            Direction = ParameterDirection.Input,
                            Value = consignmentId
                        });
                cmd.Parameters.Add(
                    new OracleParameter("p_closed_by", OracleDbType.Int32)
                        {
                            Direction = ParameterDirection.Input,
                            Value = closedById
                        });

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception e)
                {
                    return new ProcessResult(
                        false,
                        $"Failed to invoice consignment {consignmentId}. Error {e.Message}");
                }

                return new ProcessResult(
                    true,
                    $"Consignments {consignmentId} invoiced successfully");
            }
        }
    }
}
