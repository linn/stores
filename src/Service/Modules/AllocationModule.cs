﻿namespace Linn.Stores.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Allocation;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources.Allocation;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class AllocationModule : NancyModule
    {
        private readonly IAllocationFacadeService allocationFacadeService;

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
            this.allocationFacadeService = allocationFacadeService;
            this.despatchLocationFacadeService = despatchLocationFacadeService;
            this.sosAllocHeadFacadeService = sosAllocHeadFacadeService;
            this.sosAllocDetailFacadeService = sosAllocDetailFacadeService;
            this.Get("/logistics/allocations", _ => this.GetApp());
            this.Post("/logistics/allocations", _ => this.StartAllocation());
            this.Post("/logistics/allocations/finish", p => this.FinishAllocation());
            this.Post("/logistics/allocations/pick", _ => this.PickItems());
            this.Post("/logistics/allocations/unpick", _ => this.UnpickItems());
            this.Get("/logistics/despatch-locations", _ => this.GetDespatchLocations());
            this.Get("/logistics/sos-alloc-heads", _ => this.GetAllocHeads());
            this.Get("/logistics/sos-alloc-heads/{jobId:int}", p => this.GetAllocHeads(p.jobId));
            this.Get("/logistics/sos-alloc-details", _ => this.GetAllocDetails());
            this.Put("/logistics/sos-alloc-details/{id:int}", p => this.UpdateAllocDetail(p.id));
            this.Get("/logistics/allocations/despatch-picking-summary", _ => this.GetPickingSummaryReport());
            this.Get("/logistics/allocations/despatch-pallet-queue", _ => this.GetPalletQueueReport());
            this.Get("/logistics/allocations/despatch-pallet-queue/scs", _ => this.GetPalletQueueForScs());
        }

        private object GetPalletQueueReport()
        {
            return this.Negotiate
                .WithModel(this.allocationFacadeService.DespatchPalletQueueReport())
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetPickingSummaryReport()
        {
            return this.Negotiate
                .WithModel(this.allocationFacadeService.DespatchPickingSummaryReport())
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object PickItems()
        {
            var resource = this.Bind<AccountOutletRequestResource>();

            return this.Negotiate.WithModel(this.allocationFacadeService.PickItems(resource));
        }

        private object UnpickItems()
        {
            var resource = this.Bind<AccountOutletRequestResource>();

            return this.Negotiate.WithModel(this.allocationFacadeService.UnpickItems(resource));
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
