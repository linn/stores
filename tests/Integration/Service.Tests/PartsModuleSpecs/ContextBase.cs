﻿namespace Linn.Stores.Service.Tests.PartsModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Domain.LinnApps.Parts;

    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Facade.ResourceBuilders;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.Parts;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Service.Modules;
    using Linn.Stores.Service.ResponseProcessors;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase : NancyContextBase
    {
        protected IPartsFacadeService PartsFacadeService { get; private set; }

        protected IFacadeService<AssemblyTechnology, string, AssemblyTechnologyResource, AssemblyTechnologyResource>
            AssemblyTechnologyService
        {
            get; 
            private set;
        }

        protected IFacadeService<DecrementRule, string, DecrementRuleResource, DecrementRuleResource>
            DecrementRuleService
        {
            get; private set;
        }

        protected IUnitsOfMeasureService UnitsOfMeasureService { get; private set; }

        protected IProductAnalysisCodeService ProductAnalysisCodeService { get; set; }

        protected IPartRepository PartRepository { get; private set; }

        protected IRepository<ParetoClass, string> ParetoClassRepository { get; private set; }

        protected IRepository<ProductAnalysisCode, string> ProductAnalysisCodeRepository { get; private set; }

        protected IRepository<QcControl, int> QcControlRepository { get; private set; }

        protected IRepository<MechPartSource, int> MechPartSourceRepository { get; private set; }

        protected IPartService PartsDomainService { get; private set; }

        protected IFacadeService<PartTemplate, string, PartTemplateResource, PartTemplateResource> PartTemplateService
        {
            get;
            private set;
        }

        protected IFacadeFilterService<MechPartSource, int, MechPartSourceResource, MechPartSourceResource,
            MechPartSourceSearchResource> MechPartSourceService
        {
            get; private set;
        }

        protected IPartLiveService PartLiveService { get; private set; }

        protected IFacadeService<Manufacturer, string, ManufacturerResource, ManufacturerResource> ManufacturerService
        {
            get;
            private set;
        }

        protected IRepository<Manufacturer, string> ManufacturerRepository { get; private set; }

        protected IPartDataSheetValuesService DataSheetValuesService { get; private set; }

        protected IPartPack PartPack { get; private set; }

        protected IFacadeService<PartTqmsOverride, string, PartTqmsOverrideResource, PartTqmsOverrideResource>
            TqmsCategoriesService
        {
            get; private set;
        }

        protected IFacadeService<PartLibrary, string, PartLibraryResource, PartLibraryResource> PartLibrariesService
        {
            get;
            private set;
        }

        protected IAuthorisationService AuthorisationService { get; set; }

        protected IFacadeService<LibraryRef, string, LibraryRefResource, LibraryRefResource> LibraryRefService
        {
            get;
            private set;
        }

        protected IFootprintRefOptionsService FootprintRefOptionsService { get; private set; }

        [SetUp]
        public void EstablishContext()
        {
            this.PartLibrariesService =
                Substitute.For<IFacadeService<PartLibrary, string, PartLibraryResource, PartLibraryResource>>();
            this.PartsFacadeService = Substitute
                .For<IPartsFacadeService>();
            this.PartLiveService = Substitute.For<IPartLiveService>();
            this.PartsDomainService = Substitute.For<IPartService>();
            this.UnitsOfMeasureService = Substitute.For<IUnitsOfMeasureService>();
            this.ProductAnalysisCodeService = Substitute.For<IProductAnalysisCodeService>();
            this.PartRepository = Substitute.For<IPartRepository>();
            this.MechPartSourceRepository = Substitute.For<IRepository<MechPartSource, int>>();
            this.ParetoClassRepository = Substitute.For<IRepository<ParetoClass, string>>();
            this.QcControlRepository = Substitute.For<IRepository<QcControl, int>>();
            this.ProductAnalysisCodeRepository = Substitute.For<IRepository<ProductAnalysisCode, string>>();
            this.DecrementRuleService = Substitute
                .For<IFacadeService<DecrementRule, string, DecrementRuleResource, DecrementRuleResource>>();
            this.AssemblyTechnologyService = Substitute
                .For<IFacadeService<AssemblyTechnology, string, AssemblyTechnologyResource, AssemblyTechnologyResource>>();
            this.PartTemplateService = Substitute
                .For<IFacadeService<PartTemplate, string, PartTemplateResource, PartTemplateResource>>();
            this.MechPartSourceService = Substitute
                .For<IFacadeFilterService<MechPartSource, int, MechPartSourceResource, MechPartSourceResource, MechPartSourceSearchResource>>();
            this.ManufacturerService = Substitute
                .For<IFacadeService<Manufacturer, string, ManufacturerResource, ManufacturerResource>>();
            this.ManufacturerRepository = Substitute.For<IRepository<Manufacturer, string>>();
            this.DataSheetValuesService = Substitute.For<IPartDataSheetValuesService>();
            this.PartPack = Substitute.For<IPartPack>();
            this.TqmsCategoriesService = Substitute
                .For<IFacadeService<PartTqmsOverride, string, PartTqmsOverrideResource, PartTqmsOverrideResource>>();
            this.AuthorisationService = Substitute.For<IAuthorisationService>();
            this.LibraryRefService =
                Substitute.For<IFacadeService<LibraryRef, string, LibraryRefResource, LibraryRefResource>>();
            this.FootprintRefOptionsService = Substitute.For<IFootprintRefOptionsService>();
            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Dependency(this.PartLiveService);
                        with.Dependency(this.PartsFacadeService);
                        with.Dependency(this.UnitsOfMeasureService);
                        with.Dependency(this.ProductAnalysisCodeService);
                        with.Dependency(this.PartRepository);
                        with.Dependency(this.ParetoClassRepository);
                        with.Dependency(this.ProductAnalysisCodeRepository);
                        with.Dependency(this.QcControlRepository);
                        with.Dependency(this.AssemblyTechnologyService);
                        with.Dependency(this.DecrementRuleService);
                        with.Dependency(this.PartsDomainService);
                        with.Dependency(this.PartTemplateService);
                        with.Dependency(this.MechPartSourceService);
                        with.Dependency(this.MechPartSourceRepository);
                        with.Dependency(this.ManufacturerService);
                        with.Dependency(this.DataSheetValuesService);
                        with.Dependency(this.PartPack);
                        with.Dependency(this.TqmsCategoriesService);
                        with.Dependency(this.PartLibrariesService);
                        with.Dependency(this.LibraryRefService);
                        with.Dependency(this.FootprintRefOptionsService);

                        with.Dependency<IResourceBuilder<Part>>(new PartResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<Part>>>(new PartsResourceBuilder());
                        with.Dependency<IResourceBuilder<ResponseModel<PartTemplate>>>(new PartTemplateResourceBuilder(this.AuthorisationService));
                        with.Dependency<IResourceBuilder<ResponseModel<IEnumerable<PartTemplate>>>>(new PartTemplatesResourceBuilder(this.AuthorisationService));
                        with.Dependency<IResourceBuilder<UnitOfMeasure>>(new UnitOfMeasureResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<UnitOfMeasure>>>(new UnitsOfMeasureResourceBuilder());
                        with.Dependency<IResourceBuilder<ProductAnalysisCode>>(new ProductAnalysisCodeResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<ProductAnalysisCode>>>(
                            new ProductAnalysisCodesResourceBuilder());
                        with.Module<PartsModule>();
                        with.Dependency<IResourceBuilder<AssemblyTechnology>>(new AssemblyTechnologyResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<AssemblyTechnology>>>(
                            new AssemblyTechnologiesResourceBuilder());
                        with.Dependency<IResourceBuilder<DecrementRule>>(new DecrementRuleResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<MechPartSource>>>(new MechPartSourcesResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<DecrementRule>>>(
                            new DecrementRulesResourceBuilder());
                        with.Dependency<IResourceBuilder<PartLiveTest>>(
                            new PartLiveTestResourceBuilder());
                        with.Dependency<IResourceBuilder<MechPartSource>>(new MechPartSourceResourceBuilder());
                        with.Dependency<IResourceBuilder<PartDataSheet>>(new PartDataSheetResourceBuilder());
                        with.Dependency<IResourceBuilder<Manufacturer>>(new ManufacturerResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<Manufacturer>>>(
                            new ManufacturersResourceBuilder());
                        with.Dependency<IResourceBuilder<PartTqmsOverride>>(new PartTqmsOverrideResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<PartTqmsOverride>>>(
                            new PartTqmsOverridesResourceBuilder());
                        with.Dependency<IResourceBuilder<PartLibrary>>(new PartLibraryResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<PartLibrary>>>(
                            new PartLibrariesResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<LibraryRef>>>(
                            new LibraryRefsResourceBuilder());
                        with.Dependency<IResourceBuilder<IEnumerable<FootprintRefOption>>>(
                            new FootprintRefsOptionResourceBuilder());
                        with.ResponseProcessor<PartResponseProcessor>();
                        with.ResponseProcessor<PartsResponseProcessor>();
                        with.ResponseProcessor<UnitsOfMeasureResponseProcessor>();
                        with.ResponseProcessor<AssemblyTechnologiesResponseProcessor>();
                        with.ResponseProcessor<DecrementRulesResponseProcessor>();
                        with.ResponseProcessor<ProductAnalysisCodesResponseProcessor>();
                        with.ResponseProcessor<PartTemplateResponseProcessor>();
                        with.ResponseProcessor<PartTemplatesResponseProcessor>();
                        with.ResponseProcessor<PartLiveTestResponseProcessor>();
                        with.ResponseProcessor<MechPartSourceResponseProcessor>();
                        with.ResponseProcessor<ManufacturersResponseProcessor>();
                        with.ResponseProcessor<PartTqmsOverridesResponseProcessor>();
                        with.ResponseProcessor<PartLibraryResponseProcessor>();
                        with.ResponseProcessor<PartLibrariesResponseProcessor>();
                        with.ResponseProcessor<MechPartSourcesResponseProcessor>();
                        with.ResponseProcessor<LibraryRefsResponseProcessor>();
                        with.ResponseProcessor<FootprintRefOptionsResponseProcessor>();

                        with.RequestStartup(
                            (container, pipelines, context) =>
                                {
                                    var claims = new List<Claim>
                                                     {
                                                         new Claim("employee", "employees/123"),
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
