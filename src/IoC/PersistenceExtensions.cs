namespace Linn.Sales.IoC
{
    using Linn.Common.Persistence;
    using Linn.Common.Persistence.EntityFramework;
    using Linn.Stores.Persistence.LinnApps;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class PersistenceExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            return services.AddScoped<ServiceDbContext>()
                .AddTransient<DbContext>(a => a.GetService<ServiceDbContext>())
                .AddTransient<ITransactionManager, TransactionManager>();
        }
    }
}
