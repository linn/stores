namespace Linn.Stores.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Resources.Consignments;
    using Linn.Stores.Service.Models;

    using Nancy;

    public sealed class ConsignmentsModule : NancyModule
    {
        private readonly IFacadeService<Consignment, int, ConsignmentResource, ConsignmentResource> consignmentFacadeService;

        private readonly IFacadeService<Hub, int, HubResource, HubResource> hubFacadeService;

        public ConsignmentsModule(
            IFacadeService<Consignment, int, ConsignmentResource, ConsignmentResource> consignmentFacadeService,
            IFacadeService<Hub, int, HubResource, HubResource> hubFacadeService)
        {
            this.consignmentFacadeService = consignmentFacadeService;
            this.hubFacadeService = hubFacadeService;
            this.Get("/logistics/consignments", _ => this.GetConsignments());
            this.Get("/logistics/consignments/{id:int}", p => this.GetConsignment(p.id));
            this.Get("/logistics/hubs", _ => this.GetHubs());
            this.Get("/logistics/hubs/{id:int}", p => this.GetHubById(p.id));
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
