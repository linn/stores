namespace Linn.Stores.Service.Modules
{
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class LoanHeadersModule : NancyModule
    {
        private readonly ILoanHeaderService loanHeadersService;

        public LoanHeadersModule(ILoanHeaderService loanHeadersService)
        {
            this.loanHeadersService = loanHeadersService;
            this.Get("logistics/loan-headers", _ => this.GetLoanHeaders());
        }

        private object GetLoanHeaders()
        {
            var resource = this.Bind<SearchRequestResource>();

            var results = this.loanHeadersService.Search(resource.SearchTerm);

            return this.Negotiate.WithModel(results).WithMediaRangeModel("text/json", ApplicationSettings.Get);
        }
    }
}
