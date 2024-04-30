namespace Linn.Stores.Service.Modules
{
    using System.Threading.Tasks;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Common.Service.Core;
    using Linn.Common.Service.Core.Extensions;
    using Linn.Stores.Domain.LinnApps.Allocation;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources.Allocation;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Service.Models;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    public sealed class AllocationModule : IModule
    {
        private readonly IFacadeService<DespatchLocation, int, DespatchLocationResource, DespatchLocationResource>
            despatchLocationFacadeService;

        private readonly ISosAllocHeadFacadeService sosAllocHeadFacadeService;

        private readonly IFacadeFilterService<SosAllocDetail, int, SosAllocDetailResource, SosAllocDetailResource, JobIdRequestResource> sosAllocDetailFacadeService;

        public AllocationModule(
            IAllocationFacadeService allocationFacadeService,
            IFacadeService<DespatchLocation, int, DespatchLocationResource, DespatchLocationResource> despatchLocationFacadeService,
            ISosAllocHeadFacadeService sosAllocHeadFacadeService,
            IFacadeFilterService<SosAllocDetail, int, SosAllocDetailResource, SosAllocDetailResource, JobIdRequestResource> sosAllocDetailFacadeService)
        {
            this.despatchLocationFacadeService = despatchLocationFacadeService;
            this.sosAllocHeadFacadeService = sosAllocHeadFacadeService;
            this.sosAllocDetailFacadeService = sosAllocDetailFacadeService;
        }

        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            app.MapGet("/logistics/allocations", this.GetApp);
            app.MapPost("/logistics/allocations", this.StartAllocation);
            app.MapPost("/logistics/allocations/finish", p => this.FinishAllocation);
            app.MapPost("/logistics/allocations/pick", this.PickItems);
            app.MapPost("/logistics/allocations/unpick", this.UnpickItems);
            app.MapGet("/logistics/despatch-locations", this.GetDespatchLocations);
            app.MapGet("/logistics/sos-alloc-heads", this.GetAllocHeads);
            app.MapGet("/logistics/sos-alloc-heads/{jobId:int}", this.GetAllocHeads);
            app.MapGet("/logistics/sos-alloc-details", this.GetAllocDetails);
            app.MapPut("/logistics/sos-alloc-details/{id:int}", this.UpdateAllocDetail);
            app.MapGet("/logistics/allocations/despatch-picking-summary", this.GetPickingSummaryReport);
            app.MapGet("/logistics/allocations/despatch-pallet-queue", this.GetPalletQueueReport);
            app.MapGet("/logistics/allocations/despatch-pallet-queue/scs", this.GetPalletQueueForScs);
        }

        private async Task GetPalletQueueReport(
            HttpRequest req,
            HttpResponse res,
            IAllocationFacadeService allocationFacadeService)
        {
            await res.Negotiate(allocationFacadeService.DespatchPalletQueueReport());
        }

        private async Task GetPickingSummaryReport(
            HttpRequest req,
            HttpResponse res,
            IAllocationFacadeService allocationFacadeService)
        {
            await res.Negotiate(allocationFacadeService.DespatchPickingSummaryReport());
        }

        private async Task PickItems(
            HttpRequest req,
            HttpResponse res,
            AccountOutletRequestResource resource,
            IAllocationFacadeService allocationFacadeService)
        {
            await res.Negotiate(allocationFacadeService.PickItems(resource));
        }

        private async Task UnpickItems(
            HttpRequest req,
            HttpResponse res,
            AccountOutletRequestResource resource,
            IAllocationFacadeService allocationFacadeService)
        {
            await res.Negotiate(allocationFacadeService.UnpickItems(resource));
        }

        private object FinishAllocation()
        {
            var resource = this.Bind<JobIdRequestResource>();

            return this.Negotiate.WithModel(this.allocationFacadeService.FinishAllocation(resource.JobId));
        }

        private object UpdateAllocDetail(int id)
        {
            var resource = this.Bind<SosAllocDetailResource>();
            return this.Negotiate.WithModel(this.sosAllocDetailFacadeService.Update(id, resource));
        }

        private object GetAllocDetails()
        {
            var resource = this.Bind<JobIdRequestResource>();
            return this.Negotiate.WithModel(this.sosAllocDetailFacadeService.FilterBy(resource));
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
            return this.Negotiate
                .WithModel(this.sosAllocHeadFacadeService.GetAllocHeads(jobId))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
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

        private object GetPalletQueueForScs()
        {
            return this.Negotiate.WithModel(this.allocationFacadeService.DespatchPalletQueueForScs());
        }

        private object GetApp()
        {
            return this.Negotiate.WithModel(ApplicationSettings.Get()).WithView("Index");
        }
    }
}
