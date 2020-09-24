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
    using Linn.Stores.Resources.Parts;
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

        protected IFacadeService<AssemblyTechnology, string, AssemblyTechnologyResource, AssemblyTechnologyResource> 
            AssemblyTechnologyService
        {
            get; private set;
        }

        protected IFacadeService<DecrementRule, string, DecrementRuleResource, DecrementRuleResource> 
            DecrementRuleService
        {
            get; private set;
        }

        protected IPartCategoryService PartCategoriesService { get; set; }

        protected IUnitsOfMeasureService UnitsOfMeasureService { get; private set; }

        protected IProductAnalysisCodeService ProductAnalysisCodeService { get; set; }

        protected IRepository<Part, int> PartRepository { get; private set; }

        protected IRepository<ParetoClass, string> ParetoClassRepository { get; private set; }

        protected IRepository<ProductAnalysisCode, string> ProductAnalysisCodeRepository { get; private set; }

        protected IRepository<QcControl, int> QcControlRepository { get; private set; }

        protected IPartService PartsDomainService { get; private set; }

        protected IFacadeService<PartTemplate, string, PartTemplateResource, PartTemplateResource> 
            partTemplateService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.PartsFacadeService = Substitute
                .For<IFacadeService<Part, int, PartResource, PartResource>>();
            this.PartsDomainService = Substitute.For<IPartService>();
            this.PartCategoriesService = Substitute.For<IPartCategoryService>();
            this.UnitsOfMeasureService = Substitute.For<IUnitsOfMeasureService>();
            this.ProductAnalysisCodeService = Substitute.For<IProductAnalysisCodeService>();
            this.PartRepository = Substitute.For<IRepository<Part, int>>();
            this.ParetoClassRepository = Substitute.For<IRepository<ParetoClass, string>>();
            this.QcControlRepository = Substitute.For<IRepository<QcControl, int>>();
            this.ProductAnalysisCodeRepository = Substitute.For<IRepository<ProductAnalysisCode, string>>();
            this.DecrementRuleService = Substitute
                .For<IFacadeService<DecrementRule, string, DecrementRuleResource, DecrementRuleResource>>();
            this.AssemblyTechnologyService = Substitute
                .For<IFacadeService<AssemblyTechnology, string, AssemblyTechnologyResource, AssemblyTechnologyResource>>();
            this.partTemplateService = Substitute
                .For<IFacadeService<PartTemplate, string, PartTemplateResource, PartTemplateResource>>();
            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Dependency(this.PartsFacadeService);
                        with.Dependency(this.UnitsOfMeasureService);
                        with.Dependency(this.PartCategoriesService);
                        with.Dependency(this.ProductAnalysisCodeService);
                        with.Dependency(this.PartRepository);
                        with.Dependency(this.ParetoClassRepository);
                        with.Dependency(this.ProductAnalysisCodeRepository);
                        with.Dependency(this.QcControlRepository);
                        with.Dependency(this.AssemblyTechnologyService);
                        with.Dependency(this.DecrementRuleService);
                        with.Dependency(this.PartsDomainService);
                        with.Dependency(this.partTemplateService);
                        with.Dependency<IResourceBuilder<Part>>(new PartResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<Part>>>(new PartsResourceBuilder());
                        with.Dependency<IResourceBuilder<PartTemplate>>(new PartTemplateResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<PartTemplate>>>(new PartTemplatesResourceBuilder());
                        with.Dependency<IResourceBuilder<UnitOfMeasure>>(new UnitOfMeasureResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<UnitOfMeasure>>>(new UnitsOfMeasureResourceBuilder());
                        with.Dependency<IResourceBuilder<PartCategory>>(new PartCategoryResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<PartCategory>>>(new PartCategoriesResourceBuilder());
                        with.Dependency<IResourceBuilder<ProductAnalysisCode>>(new ProductAnalysisCodeResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<ProductAnalysisCode>>>(
                            new ProductAnalysisCodesResourceBuilder());
                        with.Module<PartsModule>();
                        with.Dependency<IResourceBuilder<AssemblyTechnology>>(new AssemblyTechnologyResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<AssemblyTechnology>>>(
                            new AssemblyTechnologiesResourceBuilder());
                        with.Dependency<IResourceBuilder<DecrementRule>>(new DecrementRuleResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<DecrementRule>>>(
                            new DecrementRulesResourceBuilder());
                        with.ResponseProcessor<PartResponseProcessor>();
                        with.ResponseProcessor<PartsResponseProcessor>();
                        with.ResponseProcessor<UnitsOfMeasureResponseProcessor>();
                        with.ResponseProcessor<PartCategoriesResponseProcessor>();
                        with.ResponseProcessor<AssemblyTechnologiesResponseProcessor>();
                        with.ResponseProcessor<DecrementRulesResponseProcessor>();
                        with.ResponseProcessor<ProductAnalysisCodesResponseProcessor>();
                        with.ResponseProcessor<PartTemplatesResponseProcessor>();
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