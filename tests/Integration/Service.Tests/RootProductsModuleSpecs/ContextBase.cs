namespace Linn.Stores.Service.Tests.RootProductsModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Facade.ResourceBuilders;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources;
    using Linn.Stores.Service.Modules;
    using Linn.Stores.Service.ResponseProcessors;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase : NancyContextBase
    {
        protected IRootProductService RootProductsService { get; private set; }

        protected IQueryRepository<RootProduct> RootProductRepository { get; private set; }


        [SetUp]
        public void EstablishContext()
        {
            this.RootProductsService = Substitute
                .For<IRootProductService>();

            this.RootProductRepository = Substitute
                .For<IQueryRepository<RootProduct>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.RootProductsService);
                    with.Dependency(this.RootProductRepository);
                    with.Dependency<IResourceBuilder<RootProduct>>(new RootProductResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<RootProduct>>>(new RootProductsResourceBuilder());
                    with.Module<RootProductsModule>();
                    with.ResponseProcessor<RootProductsResponseProcessor>();
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