namespace Linn.Stores.Service.Modules
{
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources.Allocation;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class AllocationModule : NancyModule
    {
        private readonly IAllocationFacadeService allocationFacadeService;

        public AllocationModule(IAllocationFacadeService allocationFacadeService)
        {
            this.allocationFacadeService = allocationFacadeService;
            this.Post("/logistics/allocations", _ => this.StartAllocation());
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
    }
}