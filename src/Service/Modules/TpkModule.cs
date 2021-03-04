namespace Linn.Stores.Service.Modules
{
    using System.Collections.Generic;

    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class TpkModule : NancyModule
    {
        private readonly ITpkService tpkService;

        public TpkModule(ITpkService tpkService)
        {
            this.tpkService = tpkService;
            this.Get("/logistics/tpk/items", _ => this.GetItems());
            this.Post("/logistics/tpk/transfer", _ => this.TransferStock());
        }

        private object GetItems()
        {
            return this.Negotiate.WithModel(this.tpkService.GetTransferableStock());
        }

        private object TransferStock()
        {
            var resource = this.Bind<IEnumerable<TransferableStockResource>>();
            return this.Negotiate.WithModel(this.tpkService.TransferStock(resource));
        }
    }
}
