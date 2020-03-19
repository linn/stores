namespace Linn.Stores.Proxy
{
    using Linn.Common.Configuration;

    public static class ConnectionStrings
    {
        public static string ManagedConnectionString()
        {
            var host = ConfigurationManager.Configuration["DATABASE_HOST"];
            var userId = ConfigurationManager.Configuration["DATABASE_USER_ID"];
            var password = ConfigurationManager.Configuration["DATABASE_PASSWORD"];
            var databaseName = ConfigurationManager.Configuration["DATABASE_NAME"];

            var dataSource = $"(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={host})(PORT=1521))(CONNECT_DATA=(SERVICE_NAME={databaseName})(SERVER=dedicated)))";
            return $"Data Source={dataSource};User Id={userId};Password={password};";
        }
    }
}