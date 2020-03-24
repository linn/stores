namespace Linn.Stores.Service.Tests.PartsModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Domain.LinnApps.Parts;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Facade.ResourceBuilders;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources;
    using Linn.Stores.Service.Modules;
    using Linn.Stores.Service.ResponseProcessors;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase : NancyContextBase
    {
        protected IFacadeService<Part, int, PartResource, PartResource> PartsFacadeService
        {
            get; private set;
        }

        protected IPartCategoryService PartCategoriesService { get; set; }

        protected IUnitsOfMeasureService UnitsOfMeasureService { get; private set; }

        protected IRepository<Part, int> PartRepository { get; private set; }

        protected IRepository<ParetoClass, string> ParetoClassRepository { get; private set; }

        protected IRepository<ProductAnalysisCode, string> ProductAnalysisCodeRepository { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.PartsFacadeService = Substitute
                .For<IFacadeService<Part, int, PartResource, PartResource>>();
            this.PartCategoriesService = Substitute.For<IPartCategoryService>();
            this.UnitsOfMeasureService = Substitute.For<IUnitsOfMeasureService>();
            this.PartRepository = Substitute.For<IRepository<Part, int>>();
            this.ParetoClassRepository = Substitute.For<IRepository<ParetoClass, string>>();
            this.ProductAnalysisCodeRepository = Substitute.For<IRepository<ProductAnalysisCode, string>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Dependency(this.PartsFacadeService);
                        with.Dependency(this.UnitsOfMeasureService);
                        with.Dependency(this.PartCategoriesService);
                        with.Dependency(this.PartRepository);
                        with.Dependency(this.ParetoClassRepository);
                        with.Dependency(this.ProductAnalysisCodeRepository);
                        with.Dependency<IResourceBuilder<Part>>(new PartResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<Part>>>(new PartsResourceBuilder());
                        with.Dependency<IResourceBuilder<UnitOfMeasure>>(new UnitOfMeasureResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<UnitOfMeasure>>>(new UnitsOfMeasureResourceBuilder());
                        with.Dependency<IResourceBuilder<PartCategory>>(new PartCategoryResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<PartCategory>>>(new PartCategoriesResourceBuilder());
                        with.Module<PartsModule>();
                        with.ResponseProcessor<PartResponseProcessor>();
                        with.ResponseProcessor<PartsResponseProcessor>();
                        with.ResponseProcessor<UnitsOfMeasureResponseProcessor>();
                        with.ResponseProcessor<PartCategoriesResponseProcessor>();

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