namespace Linn.Stores.Service.Tests.EmployeesModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Facade;
    using Linn.Stores.Facade.ResourceBuilders;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Service.Modules;
    using Linn.Stores.Service.ResponseProcessors;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase : NancyContextBase
    {
        protected IEmployeeService EmployeesService { get; private set; }

        protected IRepository<Employee, int> EmployeeRepository { get; private set; }


        [SetUp]
        public void EstablishContext()
        {
            this.EmployeesService = Substitute
                .For<IEmployeeService>();
            this.EmployeeRepository = Substitute.For<IRepository<Employee, int>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.EmployeesService);
                    with.Dependency<IResourceBuilder<Employee>>(new EmployeeResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<Employee>>>(new EmployeesResourceBuilder());

                    with.Module<EmployeesModule>();
                    with.ResponseProcessor<EmployeesResponseProcessor>();
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
