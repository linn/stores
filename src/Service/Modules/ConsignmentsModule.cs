namespace Linn.Stores.Service.Modules
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources.Consignments;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;
    using Nancy.Security;

    public sealed class ConsignmentsModule : NancyModule
    {
        private readonly IConsignmentFacadeService consignmentFacadeService;

        private readonly IFacadeService<Hub, int, HubResource, HubResource> hubFacadeService;

        private readonly IFacadeService<Carrier, string, CarrierResource, CarrierResource> carrierFacadeService;

        private readonly IFacadeService<ShippingTerm, int, ShippingTermResource, ShippingTermResource> shippingTermFacadeService;

        private readonly IFacadeService<CartonType, string, CartonTypeResource, CartonTypeResource> cartonTypeFacadeService;

        private readonly ILogisticsProcessesFacadeService logisticsProcessesFacadeService;

        private readonly ILogisticsReportsFacadeService logisticsReportsFacadeService;

        public ConsignmentsModule(
            IConsignmentFacadeService consignmentFacadeService,
            IFacadeService<Hub, int, HubResource, HubResource> hubFacadeService,
            IFacadeService<Carrier, string, CarrierResource, CarrierResource> carrierFacadeService,
            IFacadeService<ShippingTerm, int, ShippingTermResource, ShippingTermResource> shippingTermFacadeService,
            IFacadeService<CartonType, string, CartonTypeResource, CartonTypeResource> cartonTypeFacadeService,
            ILogisticsProcessesFacadeService logisticsProcessesFacadeService,
            ILogisticsReportsFacadeService logisticsReportsFacadeService)
        {
            this.cartonTypeFacadeService = cartonTypeFacadeService;
            this.logisticsProcessesFacadeService = logisticsProcessesFacadeService;
            this.logisticsReportsFacadeService = logisticsReportsFacadeService;
            this.consignmentFacadeService = consignmentFacadeService;
            this.hubFacadeService = hubFacadeService;
            this.carrierFacadeService = carrierFacadeService;
            this.shippingTermFacadeService = shippingTermFacadeService;
            this.Post("/logistics/consignments", _ => this.AddConsignment());
            this.Get("/logistics/consignments", _ => this.GetConsignments());
            this.Get("/logistics/consignments/{id:int}", p => this.GetConsignment(p.id));
            this.Get("/logistics/consignments/{id:int}/packing-list", p => this.GetPackingList(p.id));
            this.Get("/logistics/hubs", _ => this.GetHubs());
            this.Get("/logistics/hubs/{id:int}", p => this.GetHubById(p.id));
            this.Get("/logistics/carriers", _ => this.GetCarriers());
            this.Get("/logistics/carriers/{id}", p => this.GetCarrierById(p.id));
            this.Put("/logistics/consignments/{id:int}", p => this.UpdateConsignment(p.id));
            this.Get("/logistics/shipping-terms", _ => this.GetShippingTerms());
            this.Get("/logistics/shipping-terms/{id:int}", p => this.GetShippingTermById(p.id));
            this.Get("/logistics/carton-types", _ => this.GetCartonTypes());
            this.Get("/logistics/carton-types/{id*}", p => this.GetCartonTypeById(p.id));
            this.Post("/logistics/labels", _ => this.PrintLabels());
            this.Post("/logistics/print-consignment-documents", _ => this.PrintDocuments());
            this.Post("/logistics/save-consignment-documents", _ => this.SaveDocuments());
        }

        private object AddConsignment()
        {
            this.RequiresAuthentication();
            var resource = this.Bind<ConsignmentResource>();
            var result = this.consignmentFacadeService.Add(resource);
            return this.Negotiate.WithStatusCode(HttpStatusCode.Created)
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetPackingList(int id)
        {
            return this.Negotiate.WithModel(this.logisticsReportsFacadeService.GetPackingList(id));
        }

        private object PrintDocuments()
        {
            var resource = this.Bind<ConsignmentRequestResource>();
            return this.Negotiate.WithModel(this.logisticsProcessesFacadeService.PrintConsignmentDocuments(resource));
        }

        private object SaveDocuments()
        {
            var resource = this.Bind<ConsignmentRequestResource>();
            return this.Negotiate.WithModel(this.logisticsProcessesFacadeService.SaveConsignmentDocuments(resource));
        }

        private object PrintLabels()
        {
            var resource = this.Bind<LogisticsLabelRequestResource>();

            return this.Negotiate.WithModel(this.logisticsProcessesFacadeService.PrintLabel(resource));
        }

        private object GetCartonTypeById(string id)
        {
            return this.Negotiate
                .WithModel(this.cartonTypeFacadeService.GetById(id))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetCartonTypes()
        {
            var resource = this.Bind<SearchRequestResource>();

            if (!string.IsNullOrEmpty(resource.SearchTerm))
            {
                return this.Negotiate.WithModel(this.cartonTypeFacadeService.Search(resource.SearchTerm));
            }

            return this.Negotiate.WithModel(this.cartonTypeFacadeService.GetAll());
        }

        private object GetShippingTermById(int id)
        {
            return this.Negotiate
                .WithModel(this.shippingTermFacadeService.GetById(id))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetShippingTerms()
        {
            var resource = this.Bind<SearchRequestResource>();

            if (string.IsNullOrEmpty(resource.SearchTerm))
            {
                return this.Negotiate.WithModel(this.shippingTermFacadeService.GetAll());
            }
                
            var results = this.shippingTermFacadeService.Search(resource.SearchTerm);
            if (resource.ExactOnly)
            {
                return this.Negotiate.WithModel(
                    new SuccessResult<ShippingTerm>(((SuccessResult<IEnumerable<ShippingTerm>>)results).Data.FirstOrDefault()));
            }

            return this.Negotiate.WithModel(results);
        }

        private object UpdateConsignment(int id)
        {
            this.RequiresAuthentication();
            var resource = this.Bind<ConsignmentUpdateResource>();
            var result = this.consignmentFacadeService.Update(id, resource);
            return this.Negotiate.WithModel(result);
        }

        private object GetCarrierById(string id)
        {
            return this.Negotiate
                .WithModel(this.carrierFacadeService.GetById(id))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetCarriers()
        {
            return this.Negotiate.WithModel(this.carrierFacadeService.GetAll());
        }

        private object GetHubById(int id)
        {
            return this.Negotiate
                .WithModel(this.hubFacadeService.GetById(id))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetHubs()
        {
            return this.Negotiate.WithModel(this.hubFacadeService.GetAll());
        }

        private object GetConsignment(int id)
        {
            return this.Negotiate
                .WithModel(this.consignmentFacadeService.GetById(id))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetConsignments()
        {
            var resource = this.Bind<ConsignmentsRequestResource>();

            return this.Negotiate
                .WithModel(this.consignmentFacadeService.GetByRequestResource(resource))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
