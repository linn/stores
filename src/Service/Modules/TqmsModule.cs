namespace Linn.Stores.Service.Modules
{
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class TqmsModule : NancyModule
    {
        private readonly ITqmsReportsFacadeService tqmsReportsFacadeService;

        public TqmsModule(ITqmsReportsFacadeService tqmsReportsFacadeService)
        {
            this.tqmsReportsFacadeService = tqmsReportsFacadeService;
            this.Get("/inventory/tqms-category-summary", _ => this.GetApp());
            this.Get("/inventory/tqms-category-summary/report", _ => this.GetTqmsSummaryByCategory());
        }

        private object GetTqmsSummaryByCategory()
        {
            var resource = this.Bind<JobRefRequestResource>();
            return this.Negotiate.WithModel(this.tqmsReportsFacadeService.GetTqmsSummaryByCategory(resource.JobRef));
        }

        private object GetApp()
        {
            return this.Negotiate.WithModel(ApplicationSettings.Get()).WithView("Index");
        }
    }
}
