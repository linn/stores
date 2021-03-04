namespace Linn.Stores.Service.Tests.DepartmentsModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Facade.ResourceBuilders;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Service.Modules;
    using Linn.Stores.Service.ResponseProcessors;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase : NancyContextBase
    {
        protected IDepartmentsService DepartmentsService { get; private set; }

        protected INominalAccountsService NominalAccountsService { get; set; }

        protected IQueryRepository<Department> DepartmentRepository { get; private set; }


        [SetUp]
        public void EstablishContext()
        {
            this.DepartmentsService = Substitute
                .For<IDepartmentsService>();

            this.NominalAccountsService = Substitute.For<INominalAccountsService>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.DepartmentsService);
                    with.Dependency(this.NominalAccountsService);
                    with.Dependency<IResourceBuilder<Department>>(new DepartmentResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<Department>>>(new DepartmentsResourceBuilder());
                    with.Dependency<IResourceBuilder<NominalAccount>>(new NominalAccountResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<NominalAccount>>>(new NominalAccountsResourceBuilder());
                    with.Module<DepartmentsModule>();
                    with.ResponseProcessor<DepartmentsResponseProcessor>();
                    with.ResponseProcessor<NominalAccountsResponseProcessor>();
                    with.RequestStartup(
                        (container, pipelines, context) =>
                        {
                            var claims = new List<Claim>
                                                 {
                                                         new Claim(ClaimTypes.Role, "employee"),
                                                         new Claim(ClaimTypes.NameIdentifier, "test-user")
                                                 };
                            var user = new ClaimsIdentity(claims, "jwt");

                            context.CurrentUser = new ClaimsPrincipal(user);
                        });
                });

            this.Browser = new Browser(bootstrapper);
        }
    }
}