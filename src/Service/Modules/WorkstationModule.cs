namespace Linn.Stores.Service.Modules
{
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Service.Models;

    using Nancy;

    public sealed class WorkstationModule : NancyModule
    {
        private readonly IWorkstationFacadeService workstationFacadeService;

        public WorkstationModule(IWorkstationFacadeService workstationFacadeService)
        {
            this.workstationFacadeService = workstationFacadeService;
            this.Get("/logistics/workstations/top-up", _ => this.GetStatus());
        }

        private object GetStatus()
        {
            var results = this.workstationFacadeService.GetStatus();
            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
