namespace Linn.Stores.Service.Tests.InterCompanyInvoiceModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Facade.ResourceBuilders;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Service.Modules;
    using Linn.Stores.Service.ResponseProcessors;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase : NancyContextBase
    {
        protected IInterCompanyInvoiceService InterCompanyInvoiceService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.InterCompanyInvoiceService = Substitute.For<IInterCompanyInvoiceService>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Dependency(this.InterCompanyInvoiceService);
                        with.Dependency<IResourceBuilder<IEnumerable<InterCompanyInvoice>>>(
                            new InterCompanyInvoicesResourceBuilder());
                        with.Module<InterCompanyInvoiceModule>();
                        with.ResponseProcessor<InterCompanyInvoicesResponseProcessor>();
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
