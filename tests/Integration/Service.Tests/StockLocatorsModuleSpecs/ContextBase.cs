namespace Linn.Stores.Service.Tests.StockLocatorsModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Facade.ResourceBuilders;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Service.Modules;
    using Linn.Stores.Service.ResponseProcessors;
    using Linn.Stores.Service.Tests;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase : NancyContextBase
    {
        protected IStockLocatorFacadeService 
            StockLocatorFacadeService
        {
            get; private set;
        }

        [SetUp]
        public void EstablishContext()
        {
            this.StockLocatorFacadeService = 
                Substitute
                    .For<IStockLocatorFacadeService>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.StockLocatorFacadeService);
                    with.Dependency<IResourceBuilder<StockLocator>>(new StockLocatorResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<StockLocator>>>(new StockLocatorsResourceBuilder());
                    with.Module<StockLocatorsModule>();
                    with.ResponseProcessor<StockLocatorsResponseProcessor>();
                    with.ResponseProcessor<StockLocatorResponseProcessor>();
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
