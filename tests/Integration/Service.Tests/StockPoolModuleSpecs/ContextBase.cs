namespace Linn.Stores.Service.Tests.StockPoolModuleSpecs
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
        protected IFacadeService<StockPool, int, StockPoolResource, StockPoolResource> StockPoolFacadeService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.StockPoolFacadeService = Substitute.For<IFacadeService<StockPool, int, StockPoolResource, StockPoolResource>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.StockPoolFacadeService);
                    with.Dependency<IResourceBuilder<StockPool>>(new StockPoolResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<StockPool>>>(new StockPoolsResourceBuilder());
                    with.Module<StockPoolModule>();
                    with.ResponseProcessor<StockPoolsResponseProcessor>();
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
