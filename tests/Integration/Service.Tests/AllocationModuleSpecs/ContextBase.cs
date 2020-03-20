namespace Linn.Stores.Service.Tests.AllocationModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Allocation.Models;
    using Linn.Stores.Facade.ResourceBuilders;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Service.Modules;
    using Linn.Stores.Service.ResponseProcessors;
    using Linn.Stores.Service.Tests;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase : NancyContextBase
    {
        protected IAllocationFacadeService AllocationFacadeService{ get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.AllocationFacadeService = Substitute.For<IAllocationFacadeService>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.AllocationFacadeService);
                    with.Dependency<IResourceBuilder<AllocationStart>>(new AllocationStartResourceBuilder());
                    with.Module<AllocationModule>();
                    with.ResponseProcessor<AllocationStartResponseProcessor>();
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
