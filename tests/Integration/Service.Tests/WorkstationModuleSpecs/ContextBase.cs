namespace Linn.Stores.Service.Tests.WorkstationModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Workstation.Models;
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
        protected IWorkstationFacadeService WorkstationFacadeService { get; private set; }

        protected IAuthorisationService AuthorisationService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.WorkstationFacadeService = Substitute.For<IWorkstationFacadeService>();
            this.AuthorisationService = Substitute.For<IAuthorisationService>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.WorkstationFacadeService);
                    with.Dependency<IResourceBuilder<ResponseModel<WorkstationTopUpStatus>>>(new WorkstationTopUpStatusResourceBuilder(this.AuthorisationService));
                    with.Module<WorkstationModule>();
                    with.ResponseProcessor<WorkstationTopUpStatusResponseProcessor>();
                    with.RequestStartup(
                        (container, pipelines, context) =>
                        {
                            var claims = new List<Claim>
                                                 {
                                                         new Claim(ClaimTypes.Role, "employee"),
                                                         new Claim(ClaimTypes.NameIdentifier, "test-user"),
                                                         new Claim("privilege", "p1")
                                                 };

                            var user = new ClaimsIdentity(claims, "jwt");

                            context.CurrentUser = new ClaimsPrincipal(user);
                        });
                });

            this.Browser = new Browser(bootstrapper);
        }
    }
}
