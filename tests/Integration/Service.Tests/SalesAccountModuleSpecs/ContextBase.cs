namespace Linn.Stores.Service.Tests.SalesAccountModuleSpecs
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
        protected ISalesAccountService SalesAccountService { get; set; }

        [SetUp]
        public void EstablishContext()
        {
            this.SalesAccountService = Substitute.For<ISalesAccountService>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Dependency(this.SalesAccountService);
                        with.Dependency<IResourceBuilder<IEnumerable<SalesAccount>>>(new SalesAccountsResourceBuilder());
                        with.Module<SalesAccountModule>();
                        with.ResponseProcessor<SalesAccountsResponseProcessor>();
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
