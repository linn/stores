namespace Linn.Stores.Service.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.Parts;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Service.Extensions;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;
    using Nancy.Responses;
    using Nancy.Security;

    public sealed class PartsModule : NancyModule
    {
        private readonly IPartsFacadeService partsFacadeService;

        private readonly IPartService partDomainService;

        private readonly IUnitsOfMeasureService unitsOfMeasureService;

        private readonly IPartCategoryService partCategoryService;

        private readonly IProductAnalysisCodeService productAnalysisCodeService;

        private readonly IAuthorisationService authService;

        private readonly
            IFacadeService<AssemblyTechnology, string, AssemblyTechnologyResource, AssemblyTechnologyResource>
            assemblyTechnologyService;

        private readonly
            IFacadeService<DecrementRule, string, DecrementRuleResource, DecrementRuleResource>
            decrementRuleService;

        private readonly IFacadeService<PartTemplate, string, PartTemplateResource, PartTemplateResource>
            partTemplateService;

        private readonly IPartLiveService partLiveService;

        private readonly IFacadeService<MechPartSource, int, MechPartSourceResource, MechPartSourceResource>
            mechPartSourceService;

        private readonly IFacadeService<Manufacturer, string, ManufacturerResource, ManufacturerResource>
            manufacturerService;

        private readonly IPartDataSheetValuesService dataSheetsValuesService;

        private readonly IFacadeService<TqmsCategory, string, TqmsCategoryResource, TqmsCategoryResource>
            tqmsCategoriesService;

        public PartsModule(
            IPartsFacadeService partsFacadeService,
            IUnitsOfMeasureService unitsOfMeasureService,
            IPartCategoryService partCategoryService,
            IProductAnalysisCodeService productAnalysisCodeService,
            IFacadeService<AssemblyTechnology, string, AssemblyTechnologyResource, AssemblyTechnologyResource> assemblyTechnologyService,
            IFacadeService<DecrementRule, string, DecrementRuleResource, DecrementRuleResource> decrementRuleService,
            IPartService partDomainService,
            IFacadeService<PartTemplate, string, PartTemplateResource, PartTemplateResource> partTemplateService,
            IPartLiveService partLiveService,
            IFacadeService<MechPartSource, int, MechPartSourceResource, MechPartSourceResource> mechPartSourceService,
            IFacadeService<Manufacturer, string, ManufacturerResource, ManufacturerResource> manufacturerService,
            IPartDataSheetValuesService dataSheetsValuesService,
            IAuthorisationService authService,
            IFacadeService<TqmsCategory, string, TqmsCategoryResource, TqmsCategoryResource> tqmsCategoriesService)
        {
            this.partsFacadeService = partsFacadeService;
            this.partDomainService = partDomainService;
            this.authService = authService;
            this.Get("/parts/sources", _ => this.GetApp());

            this.Get("/parts/create", _ => this.Negotiate.WithModel(ApplicationSettings.Get()).WithView("Index"));
            this.Get("/parts/sources/create", _ => this.Negotiate.WithModel(ApplicationSettings.Get()).WithView("Index"));
            this.Get("/parts/{id}", parameters => this.GetPart(parameters.id));
            this.Put("/parts/{id}", parameters => this.UpdatePart(parameters.id));
            this.Get("/parts", _ => this.GetParts());
            this.Post("/parts", _ => this.AddPart());
            this.Get("/parts/dept-stock-parts", _ => this.GetDeptStockParts());

            this.Get("/inventory/parts", _ => new RedirectResponse("/parts"));
            this.Get("/inventory/parts/sources", _ => new RedirectResponse("/parts/sources"));

            this.unitsOfMeasureService = unitsOfMeasureService;
            this.Get("inventory/units-of-measure", _ => this.GetUnitsOfMeasure());

            this.partCategoryService = partCategoryService;
            this.Get("/inventory/part-categories", _ => this.GetPartCategories());

            this.partTemplateService = partTemplateService;
            this.Get("/inventory/part-templates", _ => this.GetPartTemplates());

            this.productAnalysisCodeService = productAnalysisCodeService;
            this.Get("inventory/product-analysis-codes", _ => this.GetProductAnalysisCodes());

            this.assemblyTechnologyService = assemblyTechnologyService;
            this.Get("inventory/assembly-technologies", _ => this.GetAssemblyTechnologies());

            this.decrementRuleService = decrementRuleService;
            this.Get("/inventory/decrement-rules", _ => this.GetDecrementRules());

            this.partLiveService = partLiveService;
            this.Get("/parts/can-be-made-live/{id}", parameters => this.CheckCanBeMadeLive(parameters.id));

            this.mechPartSourceService = mechPartSourceService;
            this.Get("/parts/sources/{id}", parameters => this.GetMechPartSource(parameters.id));
            this.Put("/parts/sources/{id}", parameters => this.UpdateMechPartSource(parameters.id));
            this.Post("/parts/sources", _ => this.AddMechPartSource());

            this.manufacturerService = manufacturerService;
            this.Get("/inventory/manufacturers", _ => this.GetManufacturers());
            
            this.dataSheetsValuesService = dataSheetsValuesService;
            this.Get("/parts/data-sheet-values", _ => this.GetPartDataSheetValues());

            this.tqmsCategoriesService = tqmsCategoriesService;
            this.Get("/parts/tqms-categories", _ => this.GetTqmsCategories());
        }

        public object GetApp()
        {
            return this.Negotiate.WithModel(ApplicationSettings.Get()).WithView("Index");
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
            
            IResult<IEnumerable<Part>> results;
            if (!string.IsNullOrEmpty(resource.SearchTerm))
            {
                results = resource.ExactOnly
                              ? this.partsFacadeService.GetPartByPartNumber(resource.SearchTerm)
                              : this.partsFacadeService.Search(resource.SearchTerm);
            }
            else
            {
                results = this.partsFacadeService.GetAll();
            }

            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetDeptStockParts()
        {
            var results = this.partsFacadeService.GetDeptStockPalletParts();
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
            resource.DateCreated = DateTime.Today.ToString("o");
            var userId = this.Context.CurrentUser.GetEmployeeUri().Split("/").Last();
            resource.CreatedBy = int.Parse(userId);
            try
            {
                var result = this.partsFacadeService.Add(resource);
                if (!string.IsNullOrEmpty(resource.QcOnReceipt))
                {
                    this.partDomainService.AddQcControl(resource.PartNumber, resource.CreatedBy, resource.QcInformation);
                }

                return this.Negotiate.WithModel(result)
                    .WithMediaRangeModel("text/html", ApplicationSettings.Get);
            }
            catch (CreatePartException e)
            {
                var res = new BadRequestResult<Part>(e.Message);
                return this.Negotiate.WithModel(res)
                    .WithMediaRangeModel("text/html", ApplicationSettings.Get);
            }
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

        private object GetPartTemplates()
        {
            var result = this.partTemplateService.GetAll();
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

        private object CheckCanBeMadeLive(int id)
        {
            var result = this.partLiveService.CheckIfPartCanBeMadeLive(id);
            return this.Negotiate.WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get);
        }

        private object GetMechPartSource(int id)
        {
            var result = this.mechPartSourceService.GetById(id);
            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object UpdateMechPartSource(int id)
        {
            if (!this.authService
                    .HasPermissionFor(
                        AuthorisedAction.PartAdmin,
                        this.Context.CurrentUser.GetPrivileges()))
            {
                return new UnauthorisedResult<MechPartSource>("You are not authorised to update.");
            }

            var resource = this.Bind<MechPartSourceResource>();
            var result = this.mechPartSourceService.Update(id, resource);
            return this.Negotiate.WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get);
        }

        private object AddMechPartSource()
        {
            this.RequiresAuthentication();

            if (!this.authService
                    .HasPermissionFor(
                        AuthorisedAction.PartAdmin, 
                        this.Context.CurrentUser.GetPrivileges()))
            {
                return new UnauthorisedResult<MechPartSource>("You are not authorised to create.");
            }
            
            var resource = this.Bind<MechPartSourceResource>();
            
            var result = this.mechPartSourceService.Add(resource);

            if (result.GetType() != typeof(CreatedResult<MechPartSource>) || !resource.CreatePart)
            {
                return this.Negotiate
                    .WithModel(result).WithMediaRangeModel("text/html", ApplicationSettings.Get);
            }

            var created = ((CreatedResult<MechPartSource>)result).Data;

            this.partsFacadeService.CreatePartFromSource(created.Id, created.ProposedBy.Id, resource.Part?.DataSheets);

            return this.Negotiate.WithModel(this.mechPartSourceService.GetById(created.Id))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get);
        }

        private object GetManufacturers()
        {
            var resource = this.Bind<SearchRequestResource>();
            var result = this.manufacturerService.Search(resource.SearchTerm);
            return this.Negotiate.WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get);
        }

        private object GetPartDataSheetValues()
        {
            var result = this.dataSheetsValuesService.GetAll();
            return this.Negotiate.WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get);
        }

        private object GetTqmsCategories()
        {
            return this.Negotiate.WithModel(
                    this.tqmsCategoriesService.GetAll())
                .WithMediaRangeModel("text/html", ApplicationSettings.Get);
        }
    }
}
