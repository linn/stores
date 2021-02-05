namespace Linn.Stores.Service.Tests.WarehouseModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Stores.Facade.Services;
    using Linn.Stores.Service.Modules;
    using Linn.Stores.Service.Tests;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase : NancyContextBase
    {
        protected IWarehouseFacadeService WarehouseFacadeService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.WarehouseFacadeService = Substitute.For<IWarehouseFacadeService>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.WarehouseFacadeService);
                    with.Module<WarehouseModule>();
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
