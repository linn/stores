namespace Linn.Stores.Proxy
{
    using System;
    using System.Data;

    using Linn.Common.Proxy.LinnApps;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Models;

    using Oracle.ManagedDataAccess.Client;

    public class ExportBookPack : IExportBookPack
    {
        private readonly IDatabaseService databaseService;

        public ExportBookPack(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public ProcessResult MakeExportBookFromConsignment(int consignmentId)
        {
            using (var connection = this.databaseService.GetConnection())
            {
                var cmd = new OracleCommand("export_book_pack.make_expbook_from_consignment", connection)
                              {
                                  CommandType = CommandType.StoredProcedure
                              };


                cmd.Parameters.Add(new OracleParameter("p_consignment_id", OracleDbType.Int32)
                                       {
                                           Direction = ParameterDirection.Input,
                                           Value = consignmentId
                                       });

                connection.Open();

                try
                {
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    connection.Close();
                    return new ProcessResult(false, ex.Message);
                }

                return new ProcessResult(true, $"Export book created for consignment {consignmentId}.");
            }
        }
    }
}
