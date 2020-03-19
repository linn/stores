namespace Linn.Stores.Proxy
{
    using System.Data;

    using Oracle.ManagedDataAccess.Client;

    public interface IDatabaseService
    {
        OracleConnection GetConnection();

        int GetIdSequence(string sequenceName);

        int GetNextVal(string sequenceName);

        DataSet ExecuteQuery(string sql);
    }
}
