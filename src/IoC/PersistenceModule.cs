namespace Linn.Stores.IoC
{
    using Amazon.SQS;
    using Autofac;

    using Domain.LinnApps;
    using Domain.LinnApps.Parts;

    using Linn.Common.Logging;
    using Linn.Common.Logging.AmazonSqs;
    using Linn.Common.Persistence;
    using Linn.Common.Persistence.EntityFramework;

    using Microsoft.EntityFrameworkCore;

    using Persistence.LinnApps;
    using Persistence.LinnApps.Repositories;

    public class PersistenceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ServiceDbContext>().AsSelf()
                .As<DbContext>().InstancePerRequest();
            builder.RegisterType<TransactionManager>().As<ITransactionManager>();

            builder.RegisterType<PartRepository>().As<IRepository<Part, string>>();
            builder.RegisterType<ParetoClassRepository>().As<IRepository<ParetoClass, string>>();
            builder.RegisterType<DepartmentRepository>().As<IRepository<Department, string>>();
            builder.RegisterType<ProductAnalysisCodeRepository>().As<IRepository<ProductAnalysisCode, string>>();
        }
    }
}