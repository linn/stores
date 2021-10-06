namespace Linn.Stores.Service.Tests.RsnsModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Facade.ResourceBuilders;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Service.Modules;
    using Linn.Stores.Service.ResponseProcessors;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase : NancyContextBase
    {
        protected IRsnService RsnFacadeService { get; private set; }

        protected IQueryRepository<Rsn> RsnRepository { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.RsnFacadeService = Substitute.For<IRsnService>();

            this.RsnRepository = Substitute.For<IQueryRepository<Rsn>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Dependency(this.RsnFacadeService);
                        with.Dependency(this.RsnRepository);
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

            this.Browser = new Browser(bootstrapper);
        }
    }
}
