namespace Linn.Stores.Service.Tests.StockMoveModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Domain.LinnApps.StockMove.Models;
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
        protected IAvailableStockFacadeService AvailableStockFacadeService { get; private set; }

        protected IMoveStockFacadeService MoveStockFacadeService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.AvailableStockFacadeService = Substitute.For<IAvailableStockFacadeService>();
            this.MoveStockFacadeService = Substitute.For<IMoveStockFacadeService>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.AvailableStockFacadeService);
                    with.Dependency(this.MoveStockFacadeService);
                    with.Dependency<IResourceBuilder<IEnumerable<AvailableStock>>>(new AvailableStockResourceBuilder());
                    with.Dependency<IResourceBuilder<ProcessResult>>(new ProcessResultResourceBuilder());
                    with.Module<StockMoveModule>();
                    with.ResponseProcessor<StockAvailableResponseProcessor>();
                    with.ResponseProcessor<ProcessResultResponseProcessor>();
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
