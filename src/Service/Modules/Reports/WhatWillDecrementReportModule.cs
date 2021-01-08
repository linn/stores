namespace Linn.Stores.Service.Modules.Reports
{
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class WhatWillDecrementReportModule : NancyModule
    {
        private readonly IWhatWillDecrementReportFacadeService reportService;

        public WhatWillDecrementReportModule(IWhatWillDecrementReportFacadeService reportService)
        {
            this.reportService = reportService;
            this.Get("/inventory/reports/what-will-decrement/report", _ => this.WhatWillDecrementReport());
            this.Get("/inventory/reports/what-will-decrement", _ => this.WhatWillDecrementReportOptions());
        }

        private object WhatWillDecrementReportOptions()
        {
            return this.Negotiate.WithModel(ApplicationSettings.Get()).WithView("Index");
        }

        private object WhatWillDecrementReport()
        {
            var resource = this.Bind<WhatWillDecrementReportRequestResource>();

            var results = this.reportService.GetWhatWillDecrementReport(
                resource.PartNumber,
                resource.Quantity,
                resource.TypeOfRun,
                resource.WorkStationCode);

            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get())
                .WithView("Index");
        }
    }
}
