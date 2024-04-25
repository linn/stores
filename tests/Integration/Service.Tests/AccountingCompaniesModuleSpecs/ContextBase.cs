namespace Linn.Stores.Service.Tests.AccountingCompaniesModuleSpecs
{
    using System.Net.Http;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
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

        protected IAccountingCompanyService AccountingCompanyFacadeService { get; private set; }

        protected IQueryRepository<AccountingCompany> AccountingCompanyRepository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.AccountingCompanyRepository = Substitute.For<IQueryRepository<AccountingCompany>>();
            this.AccountingCompanyFacadeService = new AccountingCompanyService(this.AccountingCompanyRepository);

            this.Client = TestClient.With<AccountingCompaniesModule>(
                services =>
                    {
                        services.AddSingleton(this.AccountingCompanyFacadeService);
                        services.AddHandlers();
                        services.AddRouting();
                    });
        }
    }
}
