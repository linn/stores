namespace Linn.Stores.Service.Tests.CarriersModuleSpecs
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
        protected ICarriersService CarriersService { get; private set; }

        protected IQueryRepository<Carrier> CarrierRepository { get; private set; }


        [SetUp]
        public void EstablishContext()
        {
            this.CarriersService = Substitute
                .For<ICarriersService>();

            this.CarrierRepository = Substitute
                .For<IQueryRepository<Carrier>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.CarriersService);
                    with.Dependency(this.CarrierRepository);
                    with.Dependency<IResourceBuilder<Carrier>>(new CarrierResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<Carrier>>>(new CarriersResourceBuilder());
                    with.Module<CarriersModule>();
                    with.ResponseProcessor<CarriersResponseProcessor>();
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