namespace Linn.Stores.Service.Tests.LoanHeadersModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Facade.ResourceBuilders;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Service.Modules;
    using Linn.Stores.Service.ResponseProcessors;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase : NancyContextBase
    {
        protected ILoanHeaderService LoanHeaderFacadeService { get; private set; }

        protected IQueryRepository<LoanHeader> LoanHeaderRepository { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.LoanHeaderFacadeService = Substitute.For<ILoanHeaderService>();

            this.LoanHeaderRepository = Substitute.For<IQueryRepository<LoanHeader>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Dependency(this.LoanHeaderFacadeService);
                        with.Dependency(this.LoanHeaderRepository);
                        with.Dependency<IResourceBuilder<LoanHeader>>(new LoanHeaderResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<LoanHeader>>>(new LoanHeadersResourceBuilder());
                        with.Module<LoanHeadersModule>();
                        with.ResponseProcessor<LoanHeadersResponseProcessor>();
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
