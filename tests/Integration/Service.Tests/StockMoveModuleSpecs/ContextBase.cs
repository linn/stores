namespace Linn.Stores.Service.Tests.StockMoveModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.StockMove.Models;
    using Linn.Stores.Facade.ResourceBuilders;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources;
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

        protected IFacadeService<PartStorageType, int, PartStorageTypeResource, PartStorageTypeResource> PartStorageTypeFacadeService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.AvailableStockFacadeService = Substitute.For<IAvailableStockFacadeService>();
            this.MoveStockFacadeService = Substitute.For<IMoveStockFacadeService>();
            this.PartStorageTypeFacadeService = Substitute.For<IFacadeService<PartStorageType, int, PartStorageTypeResource, PartStorageTypeResource>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.AvailableStockFacadeService);
                    with.Dependency(this.MoveStockFacadeService);
                    with.Dependency(this.PartStorageTypeFacadeService);
                    with.Dependency<IResourceBuilder<IEnumerable<AvailableStock>>>(new AvailableStockResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<PartStorageType>>>(new PartStorageTypesResourceBuilder());
                    with.Dependency<IResourceBuilder<RequisitionProcessResult>>(new RequisitionProcessResultResourceBuilder());
                    with.Module<StockMoveModule>();
                    with.ResponseProcessor<StockAvailableResponseProcessor>();
                    with.ResponseProcessor<RequisitionProcessResultResponseProcessor>();
                    with.ResponseProcessor<PartStorageTypesResponseProcessor>();
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
