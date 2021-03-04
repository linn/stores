namespace Linn.Stores.Service.Modules
{
    using Linn.Stores.Facade.Services;
    using Nancy;

    public sealed class TpkModule : NancyModule
    {
        private readonly ITpkService tpkService;

        public TpkModule(ITpkService tpkService)
        {
            this.tpkService = tpkService;
            this.Get("/logistics/tpk/items", _ => this.GetItems());
        }

        private object GetItems()
        {
            return this.Negotiate.WithModel(this.tpkService.GetTransferableStock());
        }
    }
}
