namespace Linn.Stores.Proxy
{
    using System.Data;

    using Linn.Common.Proxy.LinnApps;
    using Linn.Stores.Domain.LinnApps.ExternalServices;

    using Oracle.ManagedDataAccess.Client;

    public class StoragePlaceAuditPack : IStoragePlaceAuditPack
    {
        public string CreateAuditReq(
            string auditLocation,
            int createdBy,
            string department)
        {
            var connection = new OracleConnection(ConnectionStrings.ManagedConnectionString());

            var cmd = new OracleCommand("STORAGE_PLACE_AUDIT_PACK.CREATE_AUDIT_REQ_WRAPPER", connection)
                          {
                              CommandType = CommandType.StoredProcedure
                          };

            var auditLocationParameter = new OracleParameter("p_audit_location", OracleDbType.Varchar2)
                                          {
                                              Direction = ParameterDirection.Input, Size = 16, Value = auditLocation
                                          };
            cmd.Parameters.Add(auditLocationParameter);

            var createdByParameter = new OracleParameter("p_created_by", OracleDbType.Int32)
                                         {
                                             Direction = ParameterDirection.Input,
                                             Value = createdBy
                                         };
            cmd.Parameters.Add(createdByParameter);

            var departmentParameter = new OracleParameter("p_department", OracleDbType.Varchar2)
                                          {
                                              Direction = ParameterDirection.Input, Size = 10, Value = department
                                          };
            cmd.Parameters.Add(departmentParameter);

            var result = new OracleParameter("p_success", OracleDbType.Varchar2)
                             {
                                 Direction = ParameterDirection.InputOutput,
                                 Size = 2000
                             };
            cmd.Parameters.Add(result);

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

            return result.Value.ToString();
        }
    }
}