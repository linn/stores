namespace Linn.Stores.IoC
{
    using Amazon.SQS;
    using Autofac;

    using Domain.LinnApps.Parts;

    using Linn.Common.Facade;
    using Linn.Common.Logging;
    using Linn.Common.Logging.AmazonSqs;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources;

    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // facade services
            builder.RegisterType<PartFacadeService>()
                .As<IFacadeService<Part, int, PartResource, PartResource>>();
            builder.RegisterType<AccountingCompanyService>().As<IAccountingCompanyService>();
            builder.RegisterType<RootProductsService>().As<IRootProductService>();
            builder.RegisterType<DepartmentService>().As<IDepartmentsService>();
        }
    }
}