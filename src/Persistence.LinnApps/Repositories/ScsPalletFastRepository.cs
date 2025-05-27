namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Linn.Common.Proxy.LinnApps;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Scs;

    using Microsoft.EntityFrameworkCore.Internal;

    using Oracle.ManagedDataAccess.Client;
    using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

    public class ScsPalletFastRepository : IScsPalletsRepository
    {
        private readonly IDatabaseService databaseService;

        public ScsPalletFastRepository(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public void ReplaceAll(IEnumerable<ScsStorePallet> pallets)
        {
            using (var connection = this.databaseService.GetConnection())
            {
                connection.Open();
                this.RunCommand(connection, "truncate table scs_pallets");
                if (EnumerableExtensions.Any(pallets))
                {
                    // new type of crap 
                    var insertSql = "INSERT INTO SCS_PALLETS(PALLET_NUMBER, LAST_UPDATED, AREA, COL, LVL, SIDE, HEIGHT, USER_FRIENDLY_NAME) VALUES (:1,SYSDATE,:2,:3,:4,:5,:6,:7)";
                    var cmd = new OracleCommand(insertSql, connection)
                                  {
                        CommandType = CommandType.Text
                                  };
                    cmd.Parameters.Add(":1", OracleDbType.Int32, ParameterDirection.Input);
                    cmd.Parameters.Add(":2", OracleDbType.Int32, ParameterDirection.Input);
                    cmd.Parameters.Add(":3", OracleDbType.Int32, ParameterDirection.Input);
                    cmd.Parameters.Add(":4", OracleDbType.Int32, ParameterDirection.Input);
                    cmd.Parameters.Add(":5", OracleDbType.Int32, ParameterDirection.Input);
                    cmd.Parameters.Add(":6", OracleDbType.Int32, ParameterDirection.Input);
                    cmd.Parameters.Add(":7", OracleDbType.Varchar2, ParameterDirection.Input);

                    cmd.ArrayBindCount = pallets.Count();

                    cmd.Parameters[":1"].Value = pallets.Select(p => p.PalletNumber).ToArray();
                    cmd.Parameters[":2"].Value = pallets.Select(p => p.Area).ToArray();
                    cmd.Parameters[":3"].Value = pallets.Select(p => p.Column).ToArray();
                    cmd.Parameters[":4"].Value = pallets.Select(p => p.Level).ToArray();
                    cmd.Parameters[":5"].Value = pallets.Select(p => p.Side).ToArray();
                    cmd.Parameters[":6"].Value = pallets.Select(p => p.Height).ToArray();
                    cmd.Parameters[":7"].Value = pallets.Select(p => p.UserFriendlyName).ToArray();

                    cmd.ExecuteNonQuery();

                    this.RunCommand(connection,"COMMIT");
                }
                connection.Close();
            }
        }

        private void RunCommand(OracleConnection connection, string cmdString)
        {
            var cmd = new OracleCommand(cmdString, connection)
                          {
                              CommandType = CommandType.Text
                          };
            cmd.ExecuteNonQuery();
        }

        private string InsertSQL(ScsStorePallet pallet)
        {
            return $"INSERT INTO SCS_PALLETS(PALLET_NUMBER, LAST_UPDATED) VALUES ({pallet.PalletNumber}, SYSDATE)";
        }
    }
}
