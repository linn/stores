namespace Linn.Stores.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Allocation;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.Allocation;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class AllocationModule : NancyModule
    {
        private readonly IAllocationFacadeService allocationFacadeService;

        private readonly IFacadeService<DespatchLocation, int, DespatchLocationResource, DespatchLocationResource>
            despatchLocationFacadeService;

        private readonly ISosAllocHeadFacadeService sosAllocHeadFacadeService;

        public AllocationModule(
            IAllocationFacadeService allocationFacadeService,
            IFacadeService<DespatchLocation, int, DespatchLocationResource, DespatchLocationResource> despatchLocationFacadeService,
            ISosAllocHeadFacadeService sosAllocHeadFacadeService)
        {
            this.allocationFacadeService = allocationFacadeService;
            this.despatchLocationFacadeService = despatchLocationFacadeService;
            this.sosAllocHeadFacadeService = sosAllocHeadFacadeService;
            this.Get("/logistics/allocations", _ => this.GetApp());
            this.Post("/logistics/allocations", _ => this.StartAllocation());
            this.Get("/logistics/despatch-locations", _ => this.GetDespatchLocations());
            this.Get("/logistics/sos-alloc-heads", _ => this.GetAllocHeads());
            this.Get("/logistics/sos-alloc-heads/{jobId:int}", p => this.GetAllocHeads(p.jobId));
        }

        private object GetAllocHeads()
        {
            var searchResource = this.Bind<SearchRequestResource>();
            if (string.IsNullOrWhiteSpace(searchResource.SearchTerm))
            {
                return this.Negotiate.WithModel(this.sosAllocHeadFacadeService.GetAllAllocHeads());
            }
            else
            {
                return this.Negotiate.WithModel(
                    this.sosAllocHeadFacadeService.GetAllocHeads(int.Parse(searchResource.SearchTerm)));
            }
        }

        private object GetAllocHeads(int jobId)
        {
            return this.Negotiate.WithModel(this.sosAllocHeadFacadeService.GetAllocHeads(jobId));
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
