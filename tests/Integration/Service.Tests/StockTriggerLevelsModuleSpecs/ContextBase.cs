namespace Linn.Stores.Service.Tests.StockTriggerLevelsModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.StockLocators;
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
        protected IFacadeService<StockTriggerLevel, int, StockTriggerLevelsResource, StockTriggerLevelsResource> StockTriggerLevelsFaceFacadeService { get; private set; }
       
        protected IRepository<StockTriggerLevel, int> StockTriggerLevelsRepository { get; private set; }

        protected IAuthorisationService AuthorisationService { get; set; }

        [SetUp]
        public void EstablishContext()
        {
            this.StockTriggerLevelsFaceFacadeService = Substitute.For<IFacadeService<StockTriggerLevel, int, StockTriggerLevelsResource, StockTriggerLevelsResource>>();

            this.StockTriggerLevelsRepository = Substitute.For<IRepository<StockTriggerLevel, int>>();

            this.AuthorisationService = Substitute.For<IAuthorisationService>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.StockTriggerLevelsFaceFacadeService);
                    with.Dependency(this.StockTriggerLevelsRepository);
                    with.Dependency<IResourceBuilder<StockTriggerLevel>>(new StockTriggerLevelResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<StockTriggerLevel>>>(new StockTriggerLevelsResourceBuilder());
                    with.Dependency(this.AuthorisationService);
                    with.Module<StockTriggerLevelsModule>();
                    with.ResponseProcessor<StockTriggerLevelResponseProcessor>();
                    with.ResponseProcessor<StockTriggerLevelsResponseProcessor>();
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
