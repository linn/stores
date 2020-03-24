namespace Linn.Stores.Service.Tests.SuppliersModuleSpecs
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
        protected ISuppliersService SuppliersService { get; private set; }

        protected IQueryRepository<Supplier> SupplierRepository { get; private set; }


        [SetUp]
        public void EstablishContext()
        {
            this.SuppliersService = Substitute
                .For<ISuppliersService>();

            this.SupplierRepository = Substitute
                .For<IQueryRepository<Supplier>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.SuppliersService);
                    with.Dependency(this.SupplierRepository);
                    with.Dependency<IResourceBuilder<Supplier>>(new SupplierResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<Supplier>>>(new SuppliersResourceBuilder());
                    with.Module<SuppliersModule>();
                    with.ResponseProcessor<SuppliersResponseProcessor>();
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