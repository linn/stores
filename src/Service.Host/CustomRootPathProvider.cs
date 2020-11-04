namespace Linn.Stores.Service.Host
{
    using Linn.Common.Configuration;

    using Nancy;

    public class CustomRootPathProvider : IRootPathProvider
    {
        public string GetRootPath() => ConfigurationManager.Configuration["APP_PATH"];
    }
}
