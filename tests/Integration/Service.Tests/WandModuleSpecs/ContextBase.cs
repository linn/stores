namespace Linn.Stores.Service.Tests.WandModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Wand.Models;
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
        protected IWandFacadeService WandFacadeService { get; private set; }

        protected IConsignmentShipfileFacadeService ShipfileService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.WandFacadeService = Substitute.For<IWandFacadeService>();
            this.ShipfileService = Substitute.For<IConsignmentShipfileFacadeService>();
            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.WandFacadeService);
                    with.Dependency(this.ShipfileService);
                    with.Dependency<IResourceBuilder<IEnumerable<WandConsignment>>>(new WandConsignmentsResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<WandItem>>>(new WandItemsResourceBuilder());
                    with.Dependency<IResourceBuilder<WandResult>>(new WandItemResultResourceBuilder());
                    with.Dependency<IResourceBuilder<ConsignmentShipfile>>(new ConsignmentShipfileResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<ConsignmentShipfile>>>(
                        new ConsignmentShipfilesResourceBuilder());
                    with.Module<WandModule>();
                    with.ResponseProcessor<WandConsignmentsResponseProcessor>();
                    with.ResponseProcessor<WandItemsResponseProcessor>();
                    with.ResponseProcessor<WandResultResponseProcessor>();
                    with.ResponseProcessor<ConsignmentShipfileResponseProcessor>();
                    with.ResponseProcessor<ConsignmentShipfilesResponseProcessor>();
                    with.RequestStartup(
                        (container, pipelines, context) =>
                        {
                            var claims = new List<Claim>
                                                 {
                                                         new Claim(ClaimTypes.Role, "employee"),
                                                         new Claim(ClaimTypes.NameIdentifier, "test-user"),
                                                         new Claim("privilege", "p1")
                                                 };

                            var user = new ClaimsIdentity(claims, "jwt");

                            context.CurrentUser = new ClaimsPrincipal(user);
                        });
                });

            this.Browser = new Browser(bootstrapper);
        }
    }
}
