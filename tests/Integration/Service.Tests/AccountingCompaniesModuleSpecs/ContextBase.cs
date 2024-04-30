namespace Linn.Stores.Service.Tests.AccountingCompaniesModuleSpecs
{
    using System.Net.Http;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Facade.ResourceBuilders;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.IoC;
    using Linn.Stores.Service.Modules;
    using Linn.Stores.Service.Tests;

    using Microsoft.Extensions.DependencyInjection;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected HttpClient Client { get; set; }

        protected HttpResponseMessage Response { get; set; }

        protected IAccountingCompanyFacadeService AccountingCompanyFacadeFacadeService { get; private set; }

        protected IQueryRepository<AccountingCompany> AccountingCompanyRepository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.AccountingCompanyRepository = Substitute.For<IQueryRepository<AccountingCompany>>();
            this.AccountingCompanyFacadeFacadeService = new AccountingCompanyFacadeService(this.AccountingCompanyRepository, new AccountingCompanyResourceBuilder());

            this.Client = TestClient.With<AccountingCompaniesModule>(
                services =>
                    {
                        services.AddSingleton(this.AccountingCompanyFacadeFacadeService);
                        services.AddHandlers();
                        services.AddRouting();
                    });
        }
    }
}
