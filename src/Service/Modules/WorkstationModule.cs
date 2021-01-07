namespace Linn.Stores.Service.Modules
{
    using System.Linq;

    using Linn.Stores.Facade.Services;
    using Linn.Stores.Service.Extensions;
    using Linn.Stores.Service.Models;

    using Nancy;

    public sealed class WorkstationModule : NancyModule
    {
        private readonly IWorkstationFacadeService workstationFacadeService;

        public WorkstationModule(IWorkstationFacadeService workstationFacadeService)
        {
            this.workstationFacadeService = workstationFacadeService;
            this.Get("/logistics/workstations/top-up", _ => this.GetStatus());
            this.Post("/logistics/workstations/top-up/{jobRef}/run", _ => this.RunTopUp());
        }

        private object RunTopUp()
        {
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();

            var results = this.workstationFacadeService.GetStatus(privileges);
            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetStatus()
        {
            var privileges = this.Context?.CurrentUser?.GetPrivileges().ToList();

            var results = this.workstationFacadeService.GetStatus(privileges);
            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
