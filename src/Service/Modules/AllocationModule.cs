namespace Linn.Stores.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Allocation;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources.Allocation;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class AllocationModule : NancyModule
    {
        private readonly IAllocationFacadeService allocationFacadeService;

        private readonly IFacadeService<DespatchLocation, int, DespatchLocationResource, DespatchLocationResource> despatchLocationFacadeService;

        public AllocationModule(
            IAllocationFacadeService allocationFacadeService,
            IFacadeService<DespatchLocation, int, DespatchLocationResource, DespatchLocationResource> despatchLocationFacadeService)
        {
            this.allocationFacadeService = allocationFacadeService;
            this.despatchLocationFacadeService = despatchLocationFacadeService;
            this.Get("/logistics/allocations", _ => this.GetApp());
            this.Post("/logistics/allocations", _ => this.StartAllocation());
            this.Get("/logistics/despatch-locations", _ => this.GetDespatchLocations());
        }

        private object GetDespatchLocations()
        {
            return this.Negotiate.WithModel(this.despatchLocationFacadeService.GetAll());
        }

        private object StartAllocation()
        {
            var resource = this.Bind<AllocationOptionsResource>();
            var results = this.allocationFacadeService.StartAllocation(resource);
            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetApp()
        {
            return this.Negotiate.WithModel(ApplicationSettings.Get()).WithView("Index");
        }
    }
}