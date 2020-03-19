namespace Linn.Stores.IoC
{
    using Amazon.SQS;

    using Autofac;

    using Linn.Common.Logging;
    using Linn.Common.Logging.AmazonSqs;
    using Linn.Common.Persistence;
    using Linn.Common.Persistence.EntityFramework;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Parts;
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
            builder.RegisterType<DepartmentRepository>().As<IRepository<Department, string>>();
            builder.RegisterType<ProductAnalysisCodeRepository>().As<IRepository<ProductAnalysisCode, string>>();
        }
    }
}