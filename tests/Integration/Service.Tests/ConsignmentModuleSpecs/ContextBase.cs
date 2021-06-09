namespace Linn.Stores.Service.Tests.ConsignmentModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Facade.ResourceBuilders;
    using Linn.Stores.Resources.Consignments;
    using Linn.Stores.Service.Modules;
    using Linn.Stores.Service.ResponseProcessors;
    using Linn.Stores.Service.Tests;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase : NancyContextBase
    {
        protected IFacadeService<Consignment, int, ConsignmentResource, ConsignmentResource> ConsignmentFacadeService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.ConsignmentFacadeService = Substitute.For<IFacadeService<Consignment, int, ConsignmentResource, ConsignmentResource>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.ConsignmentFacadeService);
                    with.Dependency<IResourceBuilder<Consignment>>(new ConsignmentResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<Consignment>>>(new ConsignmentsResourceBuilder());
                    with.Module<ConsignmentsModule>();
                    with.ResponseProcessor<ConsignmentResponseProcessor>();
                    with.ResponseProcessor<ConsignmentsResponseProcessor>();
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
