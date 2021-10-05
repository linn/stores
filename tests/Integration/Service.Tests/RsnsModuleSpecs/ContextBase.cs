namespace Linn.Stores.Service.Tests.RsnsModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using Common.Facade;
    using Common.Persistence;
    using Domain.LinnApps;
    using Facade.ResourceBuilders;

    using Linn.Stores.Facade.Services;

    using Modules;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;
    using Resources;
    using ResponseProcessors;

    public class ContextBase : NancyContextBase
    {
        protected IRsnService RsnFacadeService { get; private set; }

        protected IQueryRepository<Rsn> RsnRepository { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            RsnFacadeService = Substitute.For<IRsnService>();

            RsnRepository = Substitute.For<IQueryRepository<Rsn>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(RsnFacadeService);
                    with.Dependency(RsnRepository);
                    with.Dependency<IResourceBuilder<Rsn>>(new RsnResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<Rsn>>>(new RsnsResourceBuilder());
                    with.Module<RsnsModule>();
                    with.ResponseProcessor<RsnsResponseProcessor>();
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

            Browser = new Browser(bootstrapper);
        }
    }
}
