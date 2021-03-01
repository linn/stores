namespace Linn.Stores.Service.Tests.RsnModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Facade.ResourceBuilders;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Service.Modules;
    using Linn.Stores.Service.ResponseProcessors;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase : NancyContextBase
    {
        protected IExportRsnService ExportRsnService { get; set; }

        [SetUp]
        public void EstablishContext()
        {
            this.ExportRsnService = Substitute.For<IExportRsnService>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Dependency(this.ExportRsnService);
                        with.Dependency<IResourceBuilder<IEnumerable<ExportRsn>>>(new ExportRsnsResourceBuilder());
                        with.Dependency<IResourceBuilder<MakeExportReturnResult>>(
                            new MakeExportReturnResultResourceBuilder());
                        with.Module<ExportRsnModule>();
                        with.ResponseProcessor<ExportRsnsResponseProcessor>();
                        with.ResponseProcessor<MakeExportReturnResultResponseProcessor>();
                        with.RequestStartup(
                            (container, pipelines, context) =>
                                {
                                    var claims = new List<Claim>
                                                     {
                                                         new Claim(ClaimTypes.Role, "employee"),
                                                         new Claim(ClaimTypes.NameIdentifier, "test-user")
                                                     };
                                    var user = new ClaimsIdentity(claims, "jwt");

                                    context.CurrentUser = new ClaimsPrincipal(user);
                                });
                    });

            this.Browser = new Browser(bootstrapper);
        }
    }
}
