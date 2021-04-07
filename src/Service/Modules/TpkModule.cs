namespace Linn.Stores.Service.Modules
{
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources.Tpk;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class TpkModule : NancyModule
    {
        private readonly ITpkFacadeService tpkFacadeService;

        public TpkModule(ITpkFacadeService tpkFacadeService)
        {
            this.tpkFacadeService = tpkFacadeService;
            this.Get("/logistics/tpk/items", _ => this.GetItems());
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
    }
}
