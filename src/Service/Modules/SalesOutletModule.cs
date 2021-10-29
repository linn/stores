namespace Linn.Stores.Service.Modules
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class SalesOutletModule : NancyModule
    {
        private readonly ISalesOutletService salesOutletService;

        public SalesOutletModule(ISalesOutletService salesOutletService)
        {
            this.salesOutletService = salesOutletService;
            this.Get("/inventory/sales-outlets", parameters => this.GetSalesOutlets());
        }

        private object GetSalesOutlets()
        {
            IResult<IEnumerable<SalesOutlet>> result;

            var resource = this.Bind<SalesOutletRequestResource>();

            result = resource.OrderNumbers?.Length > 0 
                         ? this.salesOutletService.GetByOrders(resource.OrderNumbers) 
                         : this.salesOutletService.SearchSalesOutlets(resource.SearchTerm);

            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get())
                .WithView("Index");
        }
    }
}