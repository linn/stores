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

        public ConsignmentsModule(IFacadeService<Consignment, int, ConsignmentResource, ConsignmentResource> consignmentFacadeService)
        {
            this.consignmentFacadeService = consignmentFacadeService;
            this.Get("/logistics/consignments", _ => this.GetConsignments());
            this.Get("/logistics/consignments/{id:int}", p => this.GetConsignment(p.id));
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
            return this.Negotiate.WithModel(this.consignmentFacadeService.GetAll());
        }
    }
}
