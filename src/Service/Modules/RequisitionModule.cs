namespace Linn.Stores.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Requisitions;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources.Requisitions;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class RequisitionModule : NancyModule
    {
        private readonly IRequisitionActionsFacadeService requisitionActionsFacadeService;

        private readonly IFacadeService<RequisitionHeader, int, RequisitionResource, RequisitionResource> requisitionFacadeService;

        public RequisitionModule(
            IRequisitionActionsFacadeService requisitionActionsFacadeService,
            IFacadeService<RequisitionHeader, int, RequisitionResource, RequisitionResource> requisitionFacadeService)
        {
            this.requisitionActionsFacadeService = requisitionActionsFacadeService;
            this.requisitionFacadeService = requisitionFacadeService;
            this.Get("/logistics/requisitions/{reqNumber}", p => this.GetReq(p.reqNumber));
            this.Post("/logistics/requisitions/actions/un-allocate", _ => this.Unallocate());
            this.Post("/logistics/requisitions/{reqNumber}/lines/{lineNumber}/un-allocate", p => this.Unallocate(p.reqNumber, p.lineNumber));
        }

        private object GetReq(int reqNumber)
        {
            return this.Negotiate
                .WithModel(this.requisitionFacadeService.GetById(reqNumber))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object Unallocate(int reqNumber, int lineNumber)
        {
            return this.Negotiate.WithModel(this.requisitionActionsFacadeService.Unallocate(reqNumber, lineNumber, 100));
        }

        private object Unallocate()
        {
            var resource = this.Bind<RequisitionRequestResource>();

            return this.Negotiate.WithModel(
                this.requisitionActionsFacadeService.Unallocate(
                    resource.RequisitionNumber,
                    resource.RequisitionLine,
                    resource.UserNumber));
        }
    }
}
