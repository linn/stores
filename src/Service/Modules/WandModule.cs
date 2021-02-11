namespace Linn.Stores.Service.Modules
{
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Service.Models;

    using Nancy;

    public sealed class WandModule : NancyModule
    {
        private readonly IWandFacadeService wandFacadeService;

        public WandModule(IWandFacadeService wandFacadeService)
        {
            this.wandFacadeService = wandFacadeService;
            this.Get("/logistics/wand", _ => this.GetApp());
            this.Get("/logistics/wand/consignments", _ => this.GetConsignments());
        }

        private object GetConsignments()
        {
            return this.Negotiate.WithModel(this.wandFacadeService.GetWandConsignments());
        }

        private object GetApp()
        {
            return this.Negotiate.WithModel(ApplicationSettings.Get()).WithView("Index");
        }
    }
}
