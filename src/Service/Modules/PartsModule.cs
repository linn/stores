namespace Linn.Stores.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class PartsModule : NancyModule
    {
        private readonly IFacadeService<Part, int, PartResource, PartResource> partsFacadeService;

        private readonly IUnitsOfMeasureService unitsOfMeasureService;

        private readonly IPartCategoryService partCategoryService;

        private readonly IProductAnalysisCodeService productAnalysisCodeService;

        public PartsModule(
            IFacadeService<Part, int, PartResource, PartResource> partsFacadeService,
            IUnitsOfMeasureService unitsOfMeasureService,
            IPartCategoryService partCategoryService,
            IProductAnalysisCodeService productAnalysisCodeService)
        {
            this.partsFacadeService = partsFacadeService;

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
            var resource = this.Bind<PartResource>();
            var result = this.partsFacadeService.Add(resource);
            return this.Negotiate.WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get);
        }

        private object UpdatePart(int id)
        {
            var resource = this.Bind<PartResource>();
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
            var result = this.productAnalysisCodeService.GetProductAnalysisCodes();
            return this.Negotiate.WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get);
        }
    }
}