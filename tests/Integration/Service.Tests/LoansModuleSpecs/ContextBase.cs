namespace Linn.Stores.Service.Tests.LoansModuleSpecs
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
        protected ILoanService LoanFacadeService { get; private set; }

        protected IQueryRepository<Loan> LoanHeaderRepository { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.LoanFacadeService = Substitute.For<ILoanService>();

            this.LoanHeaderRepository = Substitute.For<IQueryRepository<Loan>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Dependency(this.LoanFacadeService);
                        with.Dependency(this.LoanHeaderRepository);
                        with.Dependency<IResourceBuilder<Loan>>(new LoanResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<Loan>>>(new LoansResourceBuilder());
                        with.Module<LoansModule>();
                        with.ResponseProcessor<LoansResponseProcessor>();
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
