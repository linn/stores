namespace Linn.Stores.Proxy
{
    using System;
    using System.Data;

    using Linn.Common.Proxy.LinnApps;
    using Linn.Stores.Domain.LinnApps.ExternalServices;

    using Oracle.ManagedDataAccess.Client;

    public class AllocPack : IAllocPack
    {
        private readonly IDatabaseService databaseService;

        public AllocPack(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public string GetNotes()
        {
            using (var connection = this.databaseService.GetConnection())
            {
                connection.Open();
                var cmd = new OracleCommand("alloc_pack.get_notes", connection)
                              {
                                  CommandType = CommandType.StoredProcedure
                              };

                var notes = new OracleParameter(null, OracleDbType.Varchar2)
                                 {
                                     Direction = ParameterDirection.ReturnValue,
                                     Size = 4000
                                 };
                cmd.Parameters.Add(notes);

                cmd.ExecuteNonQuery();
                connection.Close();
                return notes.Value.ToString();
            }
        }

        public string GetSosNotes()
        {
            using (var connection = this.databaseService.GetConnection())
            {
                connection.Open();
                var cmd = new OracleCommand("alloc_pack.get_sos_notes", connection)
                              {
                                  CommandType = CommandType.StoredProcedure
                              };

                var notes = new OracleParameter(null, OracleDbType.Varchar2)
                                {
                                    Direction = ParameterDirection.ReturnValue,
                                    Size = 4000
                                };
                cmd.Parameters.Add(notes);

                cmd.ExecuteNonQuery();
                connection.Close();
                return notes.Value.ToString();
            }
        }

        public int StartAllocation(
            string stockPoolCode,
            string despatchLocation,
            int? accountId,
            int? outletNumber,
            string articleNumber,
            string accountingCompany,
            DateTime? cutOffDate,
            string countryCode,
            bool excludeUnsuppliable,
            bool excludeHold,
            bool excludeOverCredit,
            bool excludeNorthAmerica,
            bool excludeEuropeanUnion,
            out string notes,
            out string sosNotes)
        {
            using (var connection = this.databaseService.GetConnection())
            {
                connection.Open();
                var cmd = new OracleCommand("alloc_pack.start_allocation", connection)
                              {
                                  CommandType = CommandType.StoredProcedure
                              };

                var returnJobId = new OracleParameter(null, OracleDbType.Int32)
                                      {
                                          Direction = ParameterDirection.ReturnValue
                                      };
                cmd.Parameters.Add(returnJobId);

                cmd.Parameters.Add(
                    new OracleParameter("p_stock_pool_code", OracleDbType.Varchar2)
                        {
                            Direction = ParameterDirection.Input, Value = stockPoolCode, Size = 10
                        });
                cmd.Parameters.Add(
                    new OracleParameter("p_despatch_location", OracleDbType.Varchar2)
                        {
                            Direction = ParameterDirection.Input, Value = despatchLocation, Size = 10
                        });
                cmd.Parameters.Add(
                    new OracleParameter("p_account_id", OracleDbType.Int32)
                        {
                            Direction = ParameterDirection.Input, IsNullable = true, Value = accountId
                        });
                cmd.Parameters.Add(
                    new OracleParameter("p_outlet_number", OracleDbType.Int32)
                        {
                            Direction = ParameterDirection.Input, IsNullable = true, Value = outletNumber
                        });
                cmd.Parameters.Add(
                    new OracleParameter("p_article_number", OracleDbType.Varchar2)
                        {
                            Direction = ParameterDirection.Input, Value = articleNumber, Size = 14
                        });
                cmd.Parameters.Add(
                    new OracleParameter("p_accounting_company", OracleDbType.Varchar2)
                        {
                            Direction = ParameterDirection.Input, Value = accountingCompany, Size = 10
                        });
                cmd.Parameters.Add(
                    new OracleParameter("p_cut_off_date", OracleDbType.Date)
                        {
                            Direction = ParameterDirection.Input, Value = cutOffDate, IsNullable = true
                        });
                cmd.Parameters.Add(
                    new OracleParameter("p_country_code", OracleDbType.Varchar2)
                        {
                            Direction = ParameterDirection.Input, Value = countryCode, Size = 2
                        });
                cmd.Parameters.Add(
                    new OracleParameter("p_exclude_unsuppliable", OracleDbType.Varchar2)
                        {
                            Direction = ParameterDirection.Input, Value = excludeUnsuppliable ? "Y" : "N", Size = 1
                        });
                cmd.Parameters.Add(
                    new OracleParameter("p_exclude_hold", OracleDbType.Varchar2)
                        {
                            Direction = ParameterDirection.Input, Value = excludeHold ? "Y" : "N", Size = 1
                        });
                cmd.Parameters.Add(
                    new OracleParameter("p_exclude_over_credit", OracleDbType.Varchar2)
                        {
                            Direction = ParameterDirection.Input, Value = excludeOverCredit ? "Y" : "N", Size = 1
                        });
                cmd.Parameters.Add(
                    new OracleParameter("p_exclude_north_america", OracleDbType.Varchar2)
                        {
                            Direction = ParameterDirection.Input, Value = excludeNorthAmerica ? "Y" : "N", Size = 1
                        });
                cmd.Parameters.Add(
                    new OracleParameter("p_exclude_eu", OracleDbType.Varchar2)
                        {
                            Direction = ParameterDirection.Input,
                            Value = excludeEuropeanUnion ? "Y" : "N",
                            Size = 1
                        });
                var notesParam = cmd.Parameters.Add(
                    new OracleParameter("p_notes", OracleDbType.Varchar2)
                        {
                            Direction = ParameterDirection.Output,
                            Size = 4000
                        });
                var sosNotesParam = cmd.Parameters.Add(
                    new OracleParameter("p_sos_notes", OracleDbType.Varchar2)
                        {
                            Direction = ParameterDirection.Output,
                            Size = 4000
                        });

                cmd.ExecuteNonQuery();
                connection.Close();

                notes = notesParam.Value.ToString();
                sosNotes = sosNotesParam.Value.ToString();

                return int.Parse(returnJobId.Value.ToString());
            }
        }

        public void FinishAllocation(int jobId, out string notes, out string success)
        {
            using (var connection = this.databaseService.GetConnection())
            {
                connection.Open();
                var cmd = new OracleCommand("alloc_pack.finish_allocation", connection)
                              {
                                  CommandType = CommandType.StoredProcedure
                              };

                cmd.Parameters.Add(
                    new OracleParameter("p_job_id", OracleDbType.Int32)
                        {
                            Direction = ParameterDirection.Input, Value = jobId
                        });
                var notesParam = cmd.Parameters.Add(
                    new OracleParameter("p_notes", OracleDbType.Varchar2)
                        {
                            Direction = ParameterDirection.Output, Size = 4000
                        });
                var successParam = cmd.Parameters.Add(
                    new OracleParameter("p_success", OracleDbType.Varchar2)
                        {
                            Direction = ParameterDirection.Output, Size = 1
                        });

                cmd.ExecuteNonQuery();
                connection.Close();

                notes = notesParam.Value.ToString();
                success = successParam.Value.ToString();
            }
        }
    }
}
