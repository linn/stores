namespace Linn.Stores.Service.Tests.StockTriggerLevelsModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;
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
        protected IStockTriggerLevelsFacadeService StockTriggerLevelsFacadeService { get; private set; }
        
        protected IAuthorisationService AuthorisationService { get; set; }

        [SetUp]
        public void EstablishContext()
        {
            this.StockTriggerLevelsFacadeService = Substitute.For<IStockTriggerLevelsFacadeService>();

            this.AuthorisationService = Substitute.For<IAuthorisationService>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.StockTriggerLevelsFacadeService);
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
