namespace Linn.Stores.Service.Modules
{
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Service.Models;

    using Nancy;

    public sealed class AccountingCompaniesModule : NancyModule
    {
        private readonly IAccountingCompanyService accountingCompaniesService;

        public AccountingCompaniesModule(IAccountingCompanyService accountingCompaniesFacadeService)
        {
            this.accountingCompaniesService = accountingCompaniesFacadeService;
            this.Get("inventory/accounting-companies", _ => this.GetAccountingCompanies());
        }

        private object GetAccountingCompanies()
        {
            var results = this.accountingCompaniesService.GetValid();
            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}