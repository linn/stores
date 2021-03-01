namespace Linn.Stores.Service.Modules
{
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources.Requisitions;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class RequisitionModule : NancyModule
    {
        private readonly IRequisitionActionsFacadeService requisitionActionsFacadeService;

        public RequisitionModule(IRequisitionActionsFacadeService requisitionActionsFacadeService)
        {
            this.requisitionActionsFacadeService = requisitionActionsFacadeService;
            this.Post("/logistics/requisitions/actions/un-allocate", _ => this.Unallocate());
            this.Post("/logistics/requisitions/{reqNumber}/lines/{lineNumber}/un-allocate", p => this.Unallocate(p.reqNumber, p.lineNumber));
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
