namespace Linn.Stores.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class PurchaseOrdersModule : NancyModule
    {
        private readonly IFacadeService<PurchaseOrder, int, PurchaseOrderResource, PurchaseOrderResource> purchaseOrderFacadeService;

        public PurchaseOrdersModule(
            IFacadeService<PurchaseOrder, int, PurchaseOrderResource, PurchaseOrderResource> purchaseOrderFacadeService)
        {
            this.purchaseOrderFacadeService = purchaseOrderFacadeService;
            this.Get("logistics/purchase-orders", _ => this.GetPurchaseOrders());
        }

        private object GetPurchaseOrders()
        {
            var resource = this.Bind<SearchRequestResource>();

            var results = this.purchaseOrderFacadeService.Search(resource.SearchTerm);

            return this.Negotiate.WithModel(results).WithMediaRangeModel("text/json", ApplicationSettings.Get);
        }
    }
}
