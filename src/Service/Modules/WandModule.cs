namespace Linn.Stores.Service.Modules
{
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources;
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
            this.Post("/logistics/shipfiles/send-emails", _ => this.SendEmails());
            this.Delete("/logistics/shipfiles/{id}", parameters => this.DeleteShipfile(parameters.id));
            this.Post("/logistics/shipfiles", _ => this.AddShipfile());

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
            return this.Negotiate.WithModel(this.shipfileService.GetShipfiles())
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object SendEmails()
        {
            var resource = this.Bind<ConsignmentShipfilesSendEmailsRequestResource>();
            return this.Negotiate.WithModel(this.shipfileService.SendEmails(resource));
        }

        private object AddShipfile()
        {
            var resource = this.Bind<ConsignmentShipfileResource>();
            return this.Negotiate.WithModel(this.shipfileService.Add(resource));
        }

        private object DeleteShipfile(int id)
        {
            return this.Negotiate.WithModel(this.shipfileService.DeleteShipfile(id));
        }
    }
}
