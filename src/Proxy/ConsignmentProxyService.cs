namespace Linn.Stores.Proxy
{
    using System.Data;

    using Linn.Common.Proxy.LinnApps;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Models;

    using Oracle.ManagedDataAccess.Client;

    public class ConsignmentProxyService : IConsignmentProxyService
    {
        private readonly IDatabaseService databaseService;

        public ConsignmentProxyService(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public ProcessResult CanCloseAllocation(int consignmentId)
        {
            using (var connection = this.databaseService.GetConnection())
            {
                var cmd = new OracleCommand("consignment_can_be_closed_int", connection)
                              {
                                  CommandType = CommandType.StoredProcedure
                              };
                
                var successParameter = cmd.Parameters.Add(new OracleParameter(null, OracleDbType.Int32)
                                {
                                    Direction = ParameterDirection.ReturnValue
                                });

                cmd.Parameters.Add(
                    new OracleParameter("p_consignment_id", OracleDbType.Int32)
                        {
                            Direction = ParameterDirection.Input,
                            Value = consignmentId
                        });
                var messageParameter = cmd.Parameters.Add(
                    new OracleParameter("p_message", OracleDbType.Varchar2)
                        {
                            Direction = ParameterDirection.Output,
                            Size = 4000
                        });

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                return new ProcessResult(
                    int.Parse(successParameter.Value.ToString()) == 1,
                    messageParameter.Value.ToString());
            }
        }
    }
}
