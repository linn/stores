namespace Linn.Stores.Service.Modules
{
    using System;

    using Linn.Common.Facade;
    using Linn.Common.Resources;
    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Service.Extensions;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public class StoragePlaceModule : NancyModule
    {
        private readonly IStoragePlaceAuditReportFacadeService reportService;

        private readonly IAuditLocationService auditLocationService;

        private readonly IStoragePlaceService storagePlaceService;

        public StoragePlaceModule(
            IStoragePlaceAuditReportFacadeService reportService,
            IAuditLocationService auditLocationService,
            IStoragePlaceService storagePlaceService)
        {
            this.reportService = reportService;
            this.auditLocationService = auditLocationService;
            this.storagePlaceService = storagePlaceService;
            this.Get("/inventory/reports/storage-place-audit/report", _ => this.StoragePlaceAuditReport());
            this.Get("/inventory/reports/storage-place-audit", _ => this.StoragePlaceAuditReportOptions());
            this.Get("/inventory/audit-locations", _ => this.GetAuditLocations());
            this.Get("/inventory/storage-places", _ => this.GetStoragePlaces());
            this.Post("/inventory/storage-places/create-audit-reqs", _ => this.CreateAuditReqs());
        }

        private object StoragePlaceAuditReport()
        {
            var resource = this.Bind<StoragePlaceAuditReportRequestResource>();

            var results = this.reportService.GetStoragePlaceAuditReport(resource.LocationList, resource.LocationRange);

            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get())
                .WithView("Index");
        }

        private object StoragePlaceAuditReportOptions()
        {
            return this.Negotiate.WithModel(ApplicationSettings.Get()).WithView("Index");
        }

        private object GetAuditLocations()
        {
            var resource = this.Bind<SearchRequestResource>();

            var results = this.auditLocationService.GetAuditLocations(resource.SearchTerm);

            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get())
                .WithView("Index");
        }

        private object GetStoragePlaces()
        {
            var resource = this.Bind<SearchRequestResource>();

            var results = this.storagePlaceService.GetStoragePlaces(resource.SearchTerm);

            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object CreateAuditReqs()
        {
            var resource = this.Bind<CreateAuditReqsResource>();

            resource.Links = new[] { new LinkResource("created-by", this.Context?.CurrentUser?.GetEmployeeUri()) };

            try
            {
                this.storagePlaceService.CreateAuditReqs(resource);
            }
            catch (Exception e)
            {
                return this.Negotiate.WithModel(new BadRequestResult<Error>(e.Message));
            }

            return HttpStatusCode.OK;
        }
    }
}