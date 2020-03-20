namespace Linn.Stores.IoC
{
    using System.Collections.Generic;

    using Autofac;

    using Domain.LinnApps.Parts;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Facade.ResourceBuilders;

    public class ResponsesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PartResourceBuilder>().As<IResourceBuilder<Part>>();
            builder.RegisterType<PartsResourceBuilder>().As<IResourceBuilder<IEnumerable<Part>>>();
            builder.RegisterType<AccountingCompanyResourceBuilder>()
                .As<IResourceBuilder<AccountingCompany>>();
            builder.RegisterType<AccountingCompaniesResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<AccountingCompany>>>();
        }
    }
}