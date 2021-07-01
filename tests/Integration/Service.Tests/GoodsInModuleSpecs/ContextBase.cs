namespace Linn.Stores.Service.Tests.GoodsInModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Models;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Facade.ResourceBuilders;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources.StockLocators;
    using Linn.Stores.Service.Modules;
    using Linn.Stores.Service.ResponseProcessors;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase : NancyContextBase
    {
        protected IGoodsInFacadeService Service { get; private set; } 

        protected IFacadeFilterService<StorageLocation, int,
            StorageLocationResource, StorageLocationResource, StorageLocationResource> StorageLocationService
        {
            get;
            private set;
        }

        protected ISalesArticleService SalesArticleService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.Service = Substitute.For<IGoodsInFacadeService>();
            this.StorageLocationService = Substitute
                .For<IFacadeFilterService<StorageLocation, int, StorageLocationResource, StorageLocationResource, StorageLocationResource>>();
            this.SalesArticleService = Substitute.For<ISalesArticleService>();


            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Module<GoodsInModule>();

                        with.Dependency(this.Service);
                        with.Dependency(this.StorageLocationService);
                        with.Dependency(this.SalesArticleService);

                        with.Dependency<IResourceBuilder<StorageLocation>>(new StorageLocationResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<StorageLocation>>>(
                            new StorageLocationsResourceBuilder());

                        with.Dependency<IResourceBuilder<SalesArticle>>(new SalesArticleResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<SalesArticle>>>(
                            new SalesArticlesResourceBuilder());

                        with.Dependency<IResourceBuilder<LoanDetail>>(new LoanDetailResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<LoanDetail>>>(
                            new LoanDetailsResourceBuilder());

                        with.Dependency<IResourceBuilder<ProcessResult>>(new ProcessResultResourceBuilder());

                        with.Dependency<IResourceBuilder<ValidatePurchaseOrderResult>>(
                            new ValidatePurchaseOrderResultResourceBuilder());

                        with.ResponseProcessor<SalesArticlesResponseProcessor>();
                        with.ResponseProcessor<StorageLocationsResponseProcessor>();
                        with.ResponseProcessor<LoanDetailsResponseProcessor>();
                        with.ResponseProcessor<ProcessResultResponseProcessor>();
                        with.ResponseProcessor<ValidatePurchaseOrderResultResponseProcessor>();

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
