namespace Linn.Stores.Proxy
{
    using System;
    using System.Data;

    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Models;

    using Oracle.ManagedDataAccess.Client;
    using Oracle.ManagedDataAccess.Types;

    public class StoresPack : IStoresPack
    {
        private readonly IDatabaseService databaseService;

        public StoresPack(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public ProcessResult UnAllocateRequisition(int reqNumber, int? reqLineNumber, int userNumber)
        {
            var connection = this.databaseService.GetConnection();

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
                    Value = 0
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

        public void DoTpk(int locationId, int palletNumber, DateTime dateTimeStarted, out bool success)
        {
            success = false;
            using (var connection = this.databaseService.GetConnection())
            {
                connection.Open();
                var cmd = new OracleCommand("stores_oo.do_tpk", connection) //  TODO - Move function to stores_pack
                              {
                                  CommandType = CommandType.StoredProcedure
                              };

                var arg1 = new OracleParameter("p_location_id", OracleDbType.Int32)
                               {
                                   Direction = ParameterDirection.Input,
                                   Size = 50,
                                   Value = locationId
                               };
                cmd.Parameters.Add(arg1);

                var arg2 = new OracleParameter("p_pallet_number", OracleDbType.Int32)
                               {
                                   Direction = ParameterDirection.Input,
                                   Size = 50,
                                   Value = palletNumber
                               };
                cmd.Parameters.Add(arg2);

                var arg3 = new OracleParameter("p_date_started", OracleDbType.Date)
                               {
                                   Direction = ParameterDirection.Input,
                                   Value = dateTimeStarted
                               };
                cmd.Parameters.Add(arg3);

                var arg4 = new OracleParameter("p_success", OracleDbType.Boolean)
                               {
                                   Direction = ParameterDirection.InputOutput,
                                   Value = success
                               };


                cmd.Parameters.Add(arg4);
                success = ((OracleBoolean)cmd.Parameters[1].Value).IsTrue;
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public string GetErrorMessage()
        {
            using (var connection = this.databaseService.GetConnection())
            {
                connection.Open();
                var cmd = new OracleCommand("stores_oo.STORES_ERR_MESSAGE", connection) //  TODO - Move function to stores_pack
                              {
                                  CommandType = CommandType.StoredProcedure
                              };

                var result = new OracleParameter("G_ERR", OracleDbType.Int32)
                               {
                                   Direction = ParameterDirection.ReturnValue,
                                   Size = 50
                               };
                cmd.Parameters.Add(result);

                cmd.ExecuteNonQuery();
                connection.Close();
                return result.Value.ToString();
            }
        }
    }
}
