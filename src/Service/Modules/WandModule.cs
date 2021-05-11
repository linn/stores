namespace Linn.Stores.Service.Modules
{
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Resources.Wand;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class WandModule : NancyModule
    {
        private readonly IWandFacadeService wandFacadeService;

        private readonly IConsignmentShipfileFacadeService shipfileService;

        public WandModule(IWandFacadeService wandFacadeService, IConsignmentShipfileFacadeService shipfileService)
        {
            this.wandFacadeService = wandFacadeService;
            this.Get("/logistics/wand", _ => this.GetApp());
            this.Get("/logistics/wand/consignments", _ => this.GetConsignments());
            this.Get("/logistics/wand/items", _ => this.GetItems());
            this.Post("/logistics/wand/items", _ => this.WandItem());

            this.shipfileService = shipfileService;
            this.Get("/logistics/shipfiles", _ => this.GetShipfiles());
            this.Get("/logistics/shipfiles/email-details", _ => this.GetShipfileEmailDetails());
            this.Get("/logistics/shipfiles/send-emails", _ => this.SendEmails());
        }

        private object WandItem()
        {
            var resource = this.Bind<WandItemRequestResource>();
            return this.Negotiate.WithModel(this.wandFacadeService.WandItem(resource));
        }

        private object GetItems()
        {
            var resource = this.Bind<SearchRequestResource>();
            return this.Negotiate.WithModel(this.wandFacadeService.GetWandItems(int.Parse(resource.SearchTerm)));
        }

        private object GetConsignments()
        {
            return this.Negotiate.WithModel(this.wandFacadeService.GetWandConsignments());
        }

        private object GetApp()
        {
            return this.Negotiate.WithModel(ApplicationSettings.Get()).WithView("Index");
        }

        private object GetShipfiles()
        {
            return this.Negotiate.WithModel(this.shipfileService.GetShipfiles());
        }

        private object GetShipfileEmailDetails()
        {
            return this.Negotiate.WithModel(this.shipfileService.GetShipfiles());
        }

        private object SendEmails()
        {
            return this.Negotiate.WithModel(this.shipfileService.GetShipfiles());
        }
    }
}
