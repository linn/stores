namespace Linn.Stores.Proxy
{
    using System.Data;

    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Domain.LinnApps.StockMove.Models;

    using Oracle.ManagedDataAccess.Client;

    public class StoresPack : IStoresPack
    {
        public ProcessResult UnAllocateRequisition(int reqNumber, int? reqLineNumber, int userNumber)
        {
            var connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());

            var cmd = new OracleCommand("stores_pack.unalloc_req", connection)
                          {
                              CommandType = CommandType.StoredProcedure
                          };
            cmd.Parameters.Add(
                new OracleParameter("p_req_number", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Input,
                    Value = reqNumber
                });
            cmd.Parameters.Add(
                new OracleParameter("p_line_number", OracleDbType.Int32)
                    {
                        Direction = ParameterDirection.Input,
                        Value = reqLineNumber
                    });
            cmd.Parameters.Add(
                new OracleParameter("p_unalloc_by", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Input,
                    Value = userNumber
                });

            cmd.Parameters.Add(
                new OracleParameter("p_commit", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Input,
                    Value = 1
                });
            var messageParameter = new OracleParameter("p_message", OracleDbType.Varchar2)
                                       {
                                           Direction = ParameterDirection.Output,
                                           Size = 4000
                                       };
            cmd.Parameters.Add(messageParameter);

            var successParameter = new OracleParameter("p_success", OracleDbType.Int32)
                                       {
                                           Direction = ParameterDirection.Output
                                       };
            cmd.Parameters.Add(successParameter);

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

            return new ProcessResult
            {
                Message = messageParameter.Value.ToString(),
                Success = int.Parse(successParameter.Value.ToString()) == 1
            };
        }

        public RequisitionProcessResult CreateMoveReq(int userNumber)
        {
            var connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());

            var cmd = new OracleCommand("stores_pack.create_move_req", connection)
                          {
                              CommandType = CommandType.StoredProcedure
                          };

            cmd.Parameters.Add(new OracleParameter("p_created_by", OracleDbType.Int32)
                                   {
                                       Direction = ParameterDirection.Input, Value = userNumber
                                   });

            var reqNumberParameter = new OracleParameter("p_req_number", OracleDbType.Int32)
                                         {
                                             Direction = ParameterDirection.Output
                                         };
            cmd.Parameters.Add(reqNumberParameter);

            var messageParameter = new OracleParameter("p_message", OracleDbType.Varchar2)
                                       {
                                           Direction = ParameterDirection.Output,
                                           Size = 4000
                                       };
            cmd.Parameters.Add(messageParameter);

            var successParameter = new OracleParameter("p_success", OracleDbType.Int32)
                                       {
                                           Direction = ParameterDirection.Output
                                       };
            cmd.Parameters.Add(successParameter);

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

            return new RequisitionProcessResult
            {
                Message = messageParameter.Value.ToString(),
                Success = int.Parse(successParameter.Value.ToString()) == 1
            };
        }
    }
}
