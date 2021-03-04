namespace Linn.Stores.Service.Tests.StockLocatorsModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Facade.ResourceBuilders;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources;
    using Linn.Stores.Service.Modules;
    using Linn.Stores.Service.ResponseProcessors;
    using Linn.Stores.Service.Tests;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase : NancyContextBase
    {
        protected IStockLocatorFacadeService 
            StockLocatorFacadeService
        {
            get; private set;
        }

        protected IFacadeService<StorageLocation, int, StorageLocationResource, StorageLocationResource>
            StorageLocationService
        {
            get;

            private set;
        }

        protected IFacadeService<InspectedState, string, InspectedStateResource, InspectedStateResource> StateService
        {
            get; private set;
        }

        protected IStockQuantitiesService QuantitiesService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.StockLocatorFacadeService = 
                Substitute
                    .For<IStockLocatorFacadeService>();

            this.QuantitiesService = Substitute.For<IStockQuantitiesService>();

            this.StorageLocationService = Substitute
                .For<IFacadeService<StorageLocation, int, StorageLocationResource, StorageLocationResource>>();

            this.StateService = Substitute
                .For<IFacadeService<InspectedState, string, InspectedStateResource, InspectedStateResource>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.StockLocatorFacadeService);
                    with.Dependency(this.StorageLocationService);
                    with.Dependency(this.StateService);
                    with.Dependency(this.QuantitiesService);
                    with.Dependency<IResourceBuilder<StockQuantities>>(new StockQuantitiesResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<StockQuantities>>>(new StockQuantitiesListResourceBuilder());
                    with.Dependency<IResourceBuilder<InspectedState>>(new InspectedStateResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<InspectedState>>>(new InspectedStatesResourceBuilder());
                    with.Dependency<IResourceBuilder<StockLocator>>(new StockLocatorResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<StockLocator>>>(new StockLocatorsResourceBuilder());
                    with.Dependency<IResourceBuilder<StorageLocation>>(new StorageLocationResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<StorageLocation>>>(new StorageLocationsResourceBuilder());
                    with.Dependency<IResourceBuilder<StockLocatorWithStoragePlaceInfo>>(
                        new StockLocatorResourceBuilder());
                    with.Dependency<IResourceBuilder<IEnumerable<StockLocatorWithStoragePlaceInfo>>>(
                        new StockLocatorsWithStoragePlaceInfoResourceBuilder());
                    with.Module<StockLocatorsModule>();
                    with.ResponseProcessor<StockLocatorsResponseProcessor>();
                    with.ResponseProcessor<StockLocatorResponseProcessor>();
                    with.ResponseProcessor<StockLocatorsWithStoragePlaceInfoResponseProcessor>();
                    with.ResponseProcessor<StockQuantitiesListResponseProcessor>();
                    with.ResponseProcessor<StorageLocationsResponseProcessor>();
                    with.ResponseProcessor<InspectedStatesResponseProcessor>();
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
