namespace Linn.Stores.Service.Tests.RequisitionModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Requisitions;
    using Linn.Stores.Domain.LinnApps.Requisitions.Models;
    using Linn.Stores.Facade.ResourceBuilders;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources.Requisitions;
    using Linn.Stores.Service.Modules;
    using Linn.Stores.Service.ResponseProcessors;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase : NancyContextBase
    {
        protected IRequisitionActionsFacadeService RequisitionActionsFacadeService { get; private set; }

        protected IFacadeService<RequisitionHeader, int, RequisitionResource, RequisitionResource> RequisitionFacadeService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.RequisitionActionsFacadeService = Substitute.For<IRequisitionActionsFacadeService>();
            this.RequisitionFacadeService = Substitute
                .For<IFacadeService<RequisitionHeader, int, RequisitionResource, RequisitionResource>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Dependency(this.RequisitionActionsFacadeService);
                        with.Dependency(this.RequisitionFacadeService);
                        with.Dependency<IResourceBuilder<RequisitionActionResult>>(new RequisitionActionResourceBuilder());
                        with.Dependency<IResourceBuilder<RequisitionHeader>>(new RequisitionResourceBuilder());
                        with.Module<RequisitionModule>();
                        with.ResponseProcessor<RequisitionActionResponseProcessor>();
                        with.ResponseProcessor<RequisitionResponseProcessor>();
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
