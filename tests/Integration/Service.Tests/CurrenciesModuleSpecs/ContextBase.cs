namespace Linn.Stores.Service.Tests.CurrencyModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Facade.ResourceBuilders;
    using Linn.Stores.Resources;
    using Linn.Stores.Service.Modules;
    using Linn.Stores.Service.ResponseProcessors;
    using Linn.Stores.Service.Tests;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase : NancyContextBase
    {
        protected IFacadeService<Currency, string, CurrencyResource, CurrencyResource> CurrencyFacadeService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.CurrencyFacadeService = Substitute.For<IFacadeService<Currency, string, CurrencyResource, CurrencyResource>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.CurrencyFacadeService);
                    with.Dependency<IResourceBuilder<Currency>>(new CurrencyResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<Currency>>>(new CurrenciesResourceBuilder());
                    with.Module<CurrenciesModule>();
                    with.ResponseProcessor<CurrenciesResponseProcessor>();
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
