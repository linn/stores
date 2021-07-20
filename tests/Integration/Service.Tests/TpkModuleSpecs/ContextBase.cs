namespace Linn.Stores.Service.Tests.TpkModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Tpk;
    using Linn.Stores.Domain.LinnApps.Tpk.Models;
    using Linn.Stores.Facade.ResourceBuilders;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Service.Modules;
    using Linn.Stores.Service.ResponseProcessors;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase : NancyContextBase
    {
        protected ITpkFacadeService TpkFacadeService { get; private set; }

        protected IQueryRepository<TransferableStock> TransferableStockRepository { get; private set; }

        protected ITpkService DomainService { get; private set;  }

        [SetUp]
        public void EstablishContext()
        {
            this.TpkFacadeService = Substitute
                .For<ITpkFacadeService>();

            this.TransferableStockRepository = Substitute
                .For<IQueryRepository<TransferableStock>>();

            this.DomainService = Substitute.For<ITpkService>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.TpkFacadeService);
                    with.Dependency(this.TransferableStockRepository);
                    with.Dependency(this.DomainService);
                    with.Dependency<IResourceBuilder<TransferableStock>>(new TransferableStockResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<TransferableStock>>>(new TransferableStockListResourceBuilder());
                    with.Dependency<IResourceBuilder<TpkResult>>(new TpkResultResourceBuilder());
                    with.Module<TpkModule>();
                    with.ResponseProcessor<TransferableStockListResponseProcessor>();
                    with.ResponseProcessor<TpkResultsResponseProcessor>();
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
