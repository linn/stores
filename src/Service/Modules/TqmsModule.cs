namespace Linn.Stores.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Tqms;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Resources.Tqms;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class TqmsModule : NancyModule
    {
        private readonly ITqmsReportsFacadeService tqmsReportsFacadeService;

        private readonly ISingleRecordFacadeService<TqmsMaster, TqmsMasterResource> tqmsMasterFacadeService;

        public TqmsModule(
            ITqmsReportsFacadeService tqmsReportsFacadeService,
            ISingleRecordFacadeService<TqmsMaster, TqmsMasterResource> tqmsMasterFacadeService)
        {
            this.tqmsReportsFacadeService = tqmsReportsFacadeService;
            this.tqmsMasterFacadeService = tqmsMasterFacadeService;
            this.Get("/inventory/tqms-category-summary", _ => this.GetApp());
            this.Get("/inventory/tqms-category-summary/report", _ => this.GetTqmsSummaryByCategory());
            this.Get("/inventory/tqms-master", _ => this.GetTqmsMaster());
        }

        private object GetTqmsMaster()
        {
            return this.Negotiate.WithModel(this.tqmsMasterFacadeService.Get());
        }

        private object GetTqmsSummaryByCategory()
        {
            var resource = this.Bind<JobRefRequestResource>();
            return this.Negotiate.WithModel(this.tqmsReportsFacadeService.GetTqmsSummaryByCategory(resource.JobRef))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetApp()
        {
            return this.Negotiate.WithModel(ApplicationSettings.Get()).WithView("Index");
        }
    }
}
