namespace Linn.Stores.Service.Tests.CountryModuleSpecs
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
        protected IFacadeService<Country, string, CountryResource, CountryResource> CountryFacadeService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.CountryFacadeService = Substitute.For<IFacadeService<Country, string, CountryResource, CountryResource>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.CountryFacadeService);
                    with.Dependency<IResourceBuilder<Country>>(new CountryResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<Country>>>(new CountriesResourceBuilder());
                    with.Module<CountriesModule>();
                    with.ResponseProcessor<CountriesResponseProcessor>();
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
