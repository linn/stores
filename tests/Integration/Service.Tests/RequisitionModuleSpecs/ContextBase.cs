namespace Linn.Stores.Service.Tests.RequisitionModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Requisitions.Models;
    using Linn.Stores.Facade.ResourceBuilders;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Service.Modules;
    using Linn.Stores.Service.ResponseProcessors;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase : NancyContextBase
    {
        protected IRequisitionActionsFacadeService RequisitionActionsFacadeService{ get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.RequisitionActionsFacadeService = Substitute.For<IRequisitionActionsFacadeService>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Dependency(this.RequisitionActionsFacadeService);
                        with.Dependency<IResourceBuilder<RequisitionActionResult>>(new RequisitionActionResourceBuilder());
                        with.Module<RequisitionModule>();
                        with.ResponseProcessor<RequisitionActionResponseProcessor>();
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
