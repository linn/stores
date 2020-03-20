namespace Linn.Stores.IoC
{
    using Autofac;

    using Linn.Common.Persistence;
    using Linn.Common.Persistence.EntityFramework;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Domain.LinnApps.Sos;
    using Linn.Stores.Persistence.LinnApps;
    using Linn.Stores.Persistence.LinnApps.Repositories;

    using Microsoft.EntityFrameworkCore;

    public class PersistenceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ServiceDbContext>().AsSelf()
                .As<DbContext>().InstancePerRequest();
            builder.RegisterType<TransactionManager>().As<ITransactionManager>();

            builder.RegisterType<PartRepository>().As<IRepository<Part, int>>();
            builder.RegisterType<ParetoClassRepository>().As<IRepository<ParetoClass, string>>();
            builder.RegisterType<DepartmentRepository>().As<IQueryRepository<Department>>();
            builder.RegisterType<ProductAnalysisCodeRepository>().As<IRepository<ProductAnalysisCode, string>>();
            builder.RegisterType<AccountingCompanyRepository>().As<IQueryRepository<AccountingCompany>>();
            builder.RegisterType<EmployeeRepository>().As<IRepository<Employee, int>>();
            builder.RegisterType<RootProductRepository>().As<IQueryRepository<RootProduct>>();
            builder.RegisterType<SosOptionRepository>().As<IRepository<SosOption, int>>();
        }
    }
}