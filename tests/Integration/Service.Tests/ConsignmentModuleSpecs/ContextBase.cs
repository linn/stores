﻿namespace Linn.Stores.Service.Tests.ConsignmentModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Domain.LinnApps.Consignments.Models;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Facade.ResourceBuilders;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources.Consignments;
    using Linn.Stores.Service.Modules;
    using Linn.Stores.Service.ResponseProcessors;
    using Linn.Stores.Service.Tests;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase : NancyContextBase
    {
        protected IConsignmentFacadeService ConsignmentFacadeService { get; private set; }

        protected IFacadeService<Hub, int, HubResource, HubResource> HubFacadeService { get; private set; }

        protected IFacadeService<Carrier, string, CarrierResource, CarrierResource> CarrierFacadeService { get; private set; }

        protected IFacadeService<ShippingTerm, int, ShippingTermResource, ShippingTermResource> ShippingTermFacadeService { get; private set; }

        protected IFacadeService<CartonType, string, CartonTypeResource, CartonTypeResource> CartonTypeFacadeService { get; private set; }

        protected ILogisticsProcessesFacadeService LogisticsProcessesFacadeService { get; private set; }

        protected ILogisticsReportsFacadeService LogisticsReportsFacadeService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.ConsignmentFacadeService = Substitute.For<IConsignmentFacadeService>();
            this.HubFacadeService = Substitute.For<IFacadeService<Hub, int, HubResource, HubResource>>();
            this.CarrierFacadeService = Substitute.For<IFacadeService<Carrier, string, CarrierResource, CarrierResource>>();
            this.CartonTypeFacadeService = Substitute.For<IFacadeService<CartonType, string, CartonTypeResource, CartonTypeResource>>();
            this.ShippingTermFacadeService = Substitute.For<IFacadeService<ShippingTerm, int, ShippingTermResource, ShippingTermResource>>();
            this.LogisticsProcessesFacadeService = Substitute.For<ILogisticsProcessesFacadeService>();
            this.LogisticsReportsFacadeService = Substitute.For<ILogisticsReportsFacadeService>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.ConsignmentFacadeService);
                    with.Dependency(this.HubFacadeService);
                    with.Dependency(this.CarrierFacadeService);
                    with.Dependency(this.CartonTypeFacadeService);
                    with.Dependency(this.ShippingTermFacadeService);
                    with.Dependency(this.LogisticsProcessesFacadeService);
                    with.Dependency(this.LogisticsReportsFacadeService);
                    with.Dependency<IResourceBuilder<Consignment>>(new ConsignmentResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<Consignment>>>(new ConsignmentsResourceBuilder());
                    with.Dependency<IResourceBuilder<Hub>>(new HubResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<Hub>>>(new HubsResourceBuilder());
                    with.Dependency<IResourceBuilder<ShippingTerm>>(new ShippingTermResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<ShippingTerm>>>(new ShippingTermsResourceBuilder());
                    with.Dependency<IResourceBuilder<Carrier>>(new CarrierResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<Carrier>>>(new CarriersResourceBuilder());
                    with.Dependency<IResourceBuilder<CartonType>>(new CartonTypeResourceBuilder());
                    with.Dependency<IResourceBuilder<ProcessResult>>(new ProcessResultResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<CartonType>>>(new CartonTypesResourceBuilder());
                    with.Dependency<IResourceBuilder<PackingList>>(new PackingListResourceBuilder());
                    with.Module<ConsignmentsModule>();
                    with.ResponseProcessor<ConsignmentResponseProcessor>();
                    with.ResponseProcessor<ConsignmentsResponseProcessor>();
                    with.ResponseProcessor<HubResponseProcessor>();
                    with.ResponseProcessor<HubsResponseProcessor>();
                    with.ResponseProcessor<CarrierResponseProcessor>();
                    with.ResponseProcessor<CarriersResponseProcessor>();
                    with.ResponseProcessor<CartonTypeResponseProcessor>();
                    with.ResponseProcessor<CartonTypesResponseProcessor>();
                    with.ResponseProcessor<ShippingTermResponseProcessor>();
                    with.ResponseProcessor<ShippingTermsResponseProcessor>();
                    with.ResponseProcessor<ProcessResultResponseProcessor>();
                    with.ResponseProcessor<PackingListResponseProcessor>();
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
