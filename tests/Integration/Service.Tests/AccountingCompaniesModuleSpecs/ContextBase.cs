namespace Linn.Stores.Service.Tests.AccountingCompaniesModuleSpecs
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
        protected IAccountingCompanyService AccountingCompaniesService { get; private set; }

        protected IQueryRepository<AccountingCompany> AccountingCompanyRepository { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.AccountingCompaniesService = Substitute
                .For<IAccountingCompanyService>();

            this.AccountingCompanyRepository = Substitute
                .For<IQueryRepository<AccountingCompany>>();
           
            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.AccountingCompaniesService);
                    with.Dependency(this.AccountingCompanyRepository);
                    with.Dependency<IResourceBuilder<AccountingCompany>>(new AccountingCompanyResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<AccountingCompany>>>(new AccountingCompaniesResourceBuilder());
                    with.Module<AccountingCompaniesModule>();
                    with.ResponseProcessor<AccountingCompaniesResponseProcessor>();
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