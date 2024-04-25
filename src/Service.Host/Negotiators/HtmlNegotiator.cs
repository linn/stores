namespace Linn.Stores.Service.Host.Negotiators
{
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    using Linn.Common.Configuration;
    using Linn.Common.Service.Core;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Service.Models;

    using Microsoft.AspNetCore.Http;
    using Microsoft.Net.Http.Headers;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class HtmlNegotiator : IResponseNegotiator
    {
        private readonly IViewLoader viewLoader;

        private readonly ITemplateEngine templateEngine;

        public HtmlNegotiator(IViewLoader viewLoader, ITemplateEngine templateEngine)
        {
            this.viewLoader = viewLoader;
            this.templateEngine = templateEngine;
        }

        public bool CanHandle(MediaTypeHeaderValue accept)
        {
            return accept.MediaType.Equals("text/html");
        }

        public async Task Handle(HttpRequest req, HttpResponse res, object model, CancellationToken cancellationToken)
        {
            var viewName = model is ViewResponse viewResponse
                ? viewResponse.ViewName
                : "Index.cshtml";

            var view = this.viewLoader.Load(viewName);

            var jsonAppSettings = JsonConvert.SerializeObject(
                    new
                    {
                        AuthorityUri = ConfigurationManager.Configuration["AUTHORITY_URI"],
                        AppRoot = ConfigurationManager.Configuration["APP_ROOT"],
                        ProxyRoot = ConfigurationManager.Configuration["PROXY_ROOT"]
                    },
                    Formatting.Indented,
                    new JsonSerializerSettings
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                        });

            var viewModel = new ViewModel
                                {
                                    AppSettings = jsonAppSettings,
                                    BuildNumber = ConfigurationManager.Configuration["BUILD_NUMBER"]
                                };
            var compiled = this.templateEngine.Render(viewModel, view).Result;

            res.ContentType = "text/html";
            res.StatusCode = (int)HttpStatusCode.OK;

            await res.WriteAsync(compiled, cancellationToken);
        }
    }
}
