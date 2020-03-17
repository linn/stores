namespace Linn.Stores.Service.Modules
{
    using Domain.LinnApps.Parts;

    using Linn.Common.Facade;
    using Linn.Stores.Resources;
    using Linn.Stores.Service.Models;

    using Nancy;

    public sealed class PartsModule : NancyModule
    {
        private readonly IFacadeService<Part, int, PartResource, PartResource> partsFacadeService;

        public PartsModule(IFacadeService<Part, int, PartResource, PartResource> partsFacadeService)
        {
            this.partsFacadeService = partsFacadeService;
            this.Get("/parts/{id}", parameters => this.GetPart(parameters.id));
        }

        private object GetPart(int id)
        {
            var results = this.partsFacadeService.GetById(id);
            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}