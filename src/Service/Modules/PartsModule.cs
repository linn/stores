namespace Linn.Stores.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.Parts;
    using Linn.Stores.Service.Extensions;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;
    using Nancy.Security;

    public sealed class PartsModule : NancyModule
    {
        private readonly IFacadeService<Part, int, PartResource, PartResource> partsFacadeService;

        private readonly IPartService partDomainService;

        private readonly IUnitsOfMeasureService unitsOfMeasureService;

        private readonly IPartCategoryService partCategoryService;

        private readonly IProductAnalysisCodeService productAnalysisCodeService;

        private readonly
            IFacadeService<AssemblyTechnology, string, AssemblyTechnologyResource, AssemblyTechnologyResource>
            assemblyTechnologyService;

        private readonly
            IFacadeService<DecrementRule, string, DecrementRuleResource, DecrementRuleResource>
            decrementRuleService;

        public PartsModule(
            IFacadeService<Part, int, PartResource, PartResource> partsFacadeService,
            IUnitsOfMeasureService unitsOfMeasureService,
            IPartCategoryService partCategoryService,
            IProductAnalysisCodeService productAnalysisCodeService,
            IFacadeService<AssemblyTechnology, string, AssemblyTechnologyResource, AssemblyTechnologyResource> assemblyTechnologyService,
            IFacadeService<DecrementRule, string, DecrementRuleResource, DecrementRuleResource> decrementRuleService,
            IPartService partDomainService)
        {
            this.partsFacadeService = partsFacadeService;
            this.partDomainService = partDomainService;
            this.Get("/parts/create", _ => this.Negotiate.WithModel(ApplicationSettings.Get()).WithView("Index"));
            this.Get("/parts/{id}", parameters => this.GetPart(parameters.id));
            this.Put("/parts/{id}", parameters => this.UpdatePart(parameters.id));
            this.Get("/parts", _ => this.GetParts());
            this.Post("/parts", _ => this.AddPart());

            this.unitsOfMeasureService = unitsOfMeasureService;
            this.Get("inventory/units-of-measure", _ => this.GetUnitsOfMeasure());

            this.partCategoryService = partCategoryService;
            this.Get("inventory/part-categories", _ => this.GetPartCategories());

            this.productAnalysisCodeService = productAnalysisCodeService;
            this.Get("inventory/product-analysis-codes", _ => this.GetProductAnalysisCodes());
            this.Get("inventory/product-analysis-codes", _ => this.GetProductAnalysisCodes());

            this.assemblyTechnologyService = assemblyTechnologyService;
            this.Get("inventory/assembly-technologies", _ => this.GetAssemblyTechnologies());

            this.decrementRuleService = decrementRuleService;
            this.Get("inventory/decrement-rules", _ => this.GetDecrementRules());
        }

        private object GetPart(int id)
        {
            var results = this.partsFacadeService.GetById(id);
            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetParts()
        {
            var resource = this.Bind<SearchRequestResource>();
            var results = string.IsNullOrEmpty(resource.SearchTerm)
                              ? this.partsFacadeService.GetAll()
                              : this.partsFacadeService.Search(resource.SearchTerm);
            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object AddPart()
        {
            this.RequiresAuthentication();
            var resource = this.Bind<PartResource>();
            resource.UserPrivileges = this.Context.CurrentUser.GetPrivileges();
            var result = this.partsFacadeService.Add(resource);
            this.partDomainService.AddQcControl(resource.PartNumber, resource.CreatedBy, resource.QcInformation);
            return this.Negotiate.WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get);
        }

        private object UpdatePart(int id)
        {
            this.RequiresAuthentication();
            var resource = this.Bind<PartResource>();
            resource.UserPrivileges = this.Context.CurrentUser.GetPrivileges();
            var result = this.partsFacadeService.Update(id, resource);
            return this.Negotiate.WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get);
        }

        private object GetUnitsOfMeasure()
        {
            var result = this.unitsOfMeasureService.GetUnitsOfMeasure();
            return this.Negotiate.WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get);
        }

        private object GetPartCategories()
        {
            var result = this.partCategoryService.GetCategories();
            return this.Negotiate.WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get);
        }

        private object GetProductAnalysisCodes()
        {
            var resource = this.Bind<SearchRequestResource>();
            var result = this.productAnalysisCodeService.GetProductAnalysisCodes(resource.SearchTerm);
            return this.Negotiate.WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get);
        }

        private object GetDecrementRules()
        {
            var result = this.decrementRuleService.GetAll();
            return this.Negotiate.WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get);
        }

        private object GetAssemblyTechnologies()
        {
            var result = this.assemblyTechnologyService.GetAll();
            return this.Negotiate.WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get);
        }
    }
}