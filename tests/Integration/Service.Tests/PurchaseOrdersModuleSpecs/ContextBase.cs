namespace Linn.Stores.Service.Tests.PurchaseOrdersModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Facade.ResourceBuilders;
    using Linn.Stores.Resources;
    using Linn.Stores.Service.Modules;
    using Linn.Stores.Service.ResponseProcessors;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase : NancyContextBase
    {
        protected IFacadeService<PurchaseOrder, int, PurchaseOrderResource, PurchaseOrderResource> PurchaseOrderFacadeService { get; private set; }

        protected IRepository<PurchaseOrder, int> PurchaseOrderRepository { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.PurchaseOrderFacadeService = Substitute.For<IFacadeService<PurchaseOrder, int, PurchaseOrderResource, PurchaseOrderResource>>();

            this.PurchaseOrderRepository = Substitute.For<IRepository<PurchaseOrder, int>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Dependency(this.PurchaseOrderFacadeService);
                        with.Dependency(this.PurchaseOrderRepository);
                        with.Dependency<IResourceBuilder<PurchaseOrder>>(new PurchaseOrderResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<PurchaseOrder>>>(
                            new PurchaseOrdersResourceBuilder());
                        with.Module<PurchaseOrdersModule>();
                        with.ResponseProcessor<PurchaseOrderResponseProcessor>();
                        with.ResponseProcessor<PurchaseOrdersResponseProcessor>();
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
