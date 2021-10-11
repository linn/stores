namespace Linn.Stores.Service.Modules
{
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class LoansModule : NancyModule
    {
        private readonly ILoanService loanService;

        public LoansModule(ILoanService loanService)
        {
            this.loanService = loanService;
            this.Get("logistics/loans", _ => this.GetLoans());
        }

        private object GetLoans()
        {
            var resource = this.Bind<SearchRequestResource>();

            var results = this.loanService.Search(resource.SearchTerm);

            return this.Negotiate.WithModel(results).WithMediaRangeModel("text/json", ApplicationSettings.Get);
        }
    }
}
