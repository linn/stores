namespace Linn.Stores.Service.Tests.AllocationModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Allocation;
    using Linn.Stores.Domain.LinnApps.Allocation.Models;
    using Linn.Stores.Facade.ResourceBuilders;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.Allocation;
    using Linn.Stores.Service.Modules;
    using Linn.Stores.Service.ResponseProcessors;
    using Linn.Stores.Service.Tests;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase : NancyContextBase
    {
        protected IAllocationFacadeService AllocationFacadeService { get; private set; }

        protected IFacadeService<DespatchLocation, int, DespatchLocationResource, DespatchLocationResource> DespatchLocationFacadeService { get; private set; }

        protected ISosAllocHeadFacadeService SosAllocHeadFacadeService { get; private set; }

        protected IFacadeFilterService<SosAllocDetail, int, SosAllocDetailResource, SosAllocDetailResource, JobIdRequestResource> SosAllocDetailFacadeService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.AllocationFacadeService = Substitute.For<IAllocationFacadeService>();
            this.DespatchLocationFacadeService = Substitute.For<IFacadeService<DespatchLocation, int, DespatchLocationResource, DespatchLocationResource>>();
            this.SosAllocHeadFacadeService = Substitute.For<ISosAllocHeadFacadeService>();
            this.SosAllocDetailFacadeService = Substitute.For<IFacadeFilterService<SosAllocDetail, int, SosAllocDetailResource, SosAllocDetailResource, JobIdRequestResource>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.AllocationFacadeService);
                    with.Dependency(this.DespatchLocationFacadeService);
                    with.Dependency(this.SosAllocHeadFacadeService);
                    with.Dependency(this.SosAllocDetailFacadeService);
                    with.Dependency<IResourceBuilder<AllocationStart>>(new AllocationStartResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<DespatchLocation>>>(new DespatchLocationsResourceBuilder());
                    with.Dependency<IResourceBuilder<SosAllocHead>>(new SosAllocHeadResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<SosAllocHead>>>(new SosAllocHeadsResourceBuilder());
                    with.Dependency<IResourceBuilder<SosAllocDetail>>(new SosAllocDetailResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<SosAllocDetail>>>(new SosAllocDetailsResourceBuilder());
                    with.Module<AllocationModule>();
                    with.ResponseProcessor<AllocationStartResponseProcessor>();
                    with.ResponseProcessor<DespatchLocationsResponseProcessor>();
                    with.ResponseProcessor<SosAllocHeadsResponseProcessor>();
                    with.ResponseProcessor<SosAllocDetailResponseProcessor>();
                    with.ResponseProcessor<SosAllocDetailsResponseProcessor>();
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
