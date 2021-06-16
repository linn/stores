namespace Linn.Stores.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Resources.Consignments;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;
    using Nancy.Security;

    public sealed class ConsignmentsModule : NancyModule
    {
        private readonly IFacadeService<Consignment, int, ConsignmentResource, ConsignmentUpdateResource> consignmentFacadeService;

        private readonly IFacadeService<Hub, int, HubResource, HubResource> hubFacadeService;

        private readonly IFacadeService<Carrier, string, CarrierResource, CarrierResource> carrierFacadeService;

        private readonly IFacadeService<ShippingTerm, int, ShippingTermResource, ShippingTermResource> shippingTermFacadeService;

        public ConsignmentsModule(
            IFacadeService<Consignment, int, ConsignmentResource, ConsignmentUpdateResource> consignmentFacadeService,
            IFacadeService<Hub, int, HubResource, HubResource> hubFacadeService,
            IFacadeService<Carrier, string, CarrierResource, CarrierResource> carrierFacadeService,
            IFacadeService<ShippingTerm, int, ShippingTermResource, ShippingTermResource> shippingTermFacadeService)
        {
            this.consignmentFacadeService = consignmentFacadeService;
            this.hubFacadeService = hubFacadeService;
            this.carrierFacadeService = carrierFacadeService;
            this.shippingTermFacadeService = shippingTermFacadeService;
            this.Get("/logistics/consignments", _ => this.GetConsignments());
            this.Get("/logistics/consignments/{id:int}", p => this.GetConsignment(p.id));
            this.Get("/logistics/hubs", _ => this.GetHubs());
            this.Get("/logistics/hubs/{id:int}", p => this.GetHubById(p.id));
            this.Get("/logistics/carriers", _ => this.GetCarriers());
            this.Get("/logistics/carriers/{id}", p => this.GetCarrierById(p.id));
            this.Put("/logistics/consignments/{id:int}", p => this.UpdateConsignment(p.id));
            this.Get("/logistics/shipping-terms", _ => this.GetShippingTerms());
            this.Get("/logistics/shipping-terms/{id:int}", p => this.GetShippingTermById(p.id));
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
            return this.Negotiate.WithModel(this.shippingTermFacadeService.GetAll());
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
            return this.Negotiate
                .WithModel(this.consignmentFacadeService.GetAll())
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
