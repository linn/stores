namespace Linn.Stores.Service.Modules
{
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Resources.Tpk;
    using Linn.Stores.Service.Extensions;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class TpkModule : NancyModule
    {
        private readonly ITpkFacadeService tpkFacadeService;

        public TpkModule(ITpkFacadeService tpkFacadeService)
        {
            this.tpkFacadeService = tpkFacadeService;
            this.Get("/logistics/tpk/items", _ => this.GetItems());
            this.Post("/logistics/tpk/unpick-stock", _ => this.UnpickStock());
            this.Post("/logistics/tpk/unallocate-req", _ => this.UnallocateReq());
            this.Post("/logistics/tpk/transfer", _ => this.TransferStock());
        }

        private object GetItems()
        {
            return this.Negotiate.WithModel(this.tpkFacadeService.GetTransferableStock());
        }

        private object TransferStock()
        {
            var resource = this.Bind<TpkRequestResource>();
            return this.Negotiate.WithModel(this.tpkFacadeService.TransferStock(resource));
        }

        private object UnpickStock()
        {
            var resource = this.Bind<UnpickStockRequestResource>();
            return this.Negotiate.WithModel(this.tpkFacadeService.UnpickStock(resource));
        }

        private object UnallocateReq()
        {
            var resource = this.Bind<UnallocateReqRequestResource>();
            return this.Negotiate.WithModel(this.tpkFacadeService.UnallocateReq(resource));
        }
    }
}
