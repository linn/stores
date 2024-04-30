namespace Linn.Stores.IoC
{
    using Linn.Common.Authorisation;
    using Linn.Stores.Facade.Services;

    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceExtensions
    {
        public static IServiceCollection AddFacade(this IServiceCollection services)
        {
            return services
                .AddTransient<IPartsFacadeService, PartFacadeService>()
                .AddTransient<IAccountingCompanyFacadeService, AccountingCompanyFacadeService>()
                .AddTransient<ISalesOutletService, SalesOutletService>()
                .AddTransient<IRootProductService, RootProductsService>()
                ;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddTransient<IAuthorisationService, AuthorisationService>();
        }
    }
}
