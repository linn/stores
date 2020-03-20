namespace Linn.Stores.IoC
{
    using System.Collections.Generic;

    using Autofac;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Allocation.Models;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Facade.ResourceBuilders;

    public class ResponsesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // resource builders
            builder.RegisterType<AllocationStartResourceBuilder>().As<IResourceBuilder<AllocationStart>>();
            builder.RegisterType<PartResourceBuilder>().As<IResourceBuilder<Part>>();
            builder.RegisterType<PartsResourceBuilder>().As<IResourceBuilder<IEnumerable<Part>>>();
            builder.RegisterType<AccountingCompanyResourceBuilder>()
                .As<IResourceBuilder<AccountingCompany>>();
            builder.RegisterType<AccountingCompaniesResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<AccountingCompany>>>();
            builder.RegisterType<RootProductResourceBuilder>()
                .As<IResourceBuilder<RootProduct>>();
            builder.RegisterType<RootProductsResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<RootProduct>>>();
            builder.RegisterType<DepartmentResourceBuilder>()
                .As<IResourceBuilder<Department>>();
            builder.RegisterType<DepartmentsResourceBuilder>()
                .As<IResourceBuilder<IEnumerable<Department>>>();
        }
    }
}