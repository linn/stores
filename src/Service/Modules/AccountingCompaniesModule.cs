namespace Linn.Stores.Service.Modules
{
    using System.Threading.Tasks;

    using Linn.Common.Service.Core;
    using Linn.Common.Service.Core.Extensions;
    using Linn.Stores.Facade.Services;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    public sealed class AccountingCompaniesModule : IModule
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            app.MapGet("inventory/accounting-companies", this.GetAccountingCompanies);
        }

        private async Task GetAccountingCompanies(
            HttpRequest req,
            HttpResponse res,
            IAccountingCompanyFacadeService accountingCompaniesFacadeFacadeService)
        {
            await res.Negotiate(accountingCompaniesFacadeFacadeService.GetValid());
        }
    }
}
