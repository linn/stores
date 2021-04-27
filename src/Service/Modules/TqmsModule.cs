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

        private readonly IFacadeService<TqmsJobRef, string, TqmsJobRefResource, TqmsJobRefResource> tqmsJobRefsFacadeService;

        public TqmsModule(
            ITqmsReportsFacadeService tqmsReportsFacadeService,
            ISingleRecordFacadeService<TqmsMaster, TqmsMasterResource> tqmsMasterFacadeService,
            IFacadeService<TqmsJobRef, string, TqmsJobRefResource, TqmsJobRefResource> tqmsJobRefsFacadeService)
        {
            this.tqmsReportsFacadeService = tqmsReportsFacadeService;
            this.tqmsMasterFacadeService = tqmsMasterFacadeService;
            this.tqmsJobRefsFacadeService = tqmsJobRefsFacadeService;
            this.Get("/inventory/tqms-category-summary", _ => this.GetApp());
            this.Get("/inventory/tqms-category-summary/report", _ => this.GetTqmsSummaryByCategory());
            this.Get("/inventory/tqms-master", _ => this.GetTqmsMaster());
            this.Get("/inventory/tqms-jobrefs", _ => this.GetTqmsJobrefs());
        }

        private object GetTqmsJobrefs()
        {
            return this.Negotiate.WithModel(this.tqmsJobRefsFacadeService.GetAll());
        }

        private object GetTqmsMaster()
        {
            return this.Negotiate.WithModel(this.tqmsMasterFacadeService.Get());
        }

        private object GetTqmsSummaryByCategory()
        {
            var resource = this.Bind<TqmsSummaryRequestResource>();
            return this.Negotiate.WithModel(this.tqmsReportsFacadeService.GetTqmsSummaryByCategory(resource))
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetApp()
        {
            return this.Negotiate.WithModel(ApplicationSettings.Get()).WithView("Index");
        }
    }
}
