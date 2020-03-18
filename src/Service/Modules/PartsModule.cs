namespace Linn.Stores.Service.Modules
{
    using Domain.LinnApps.Parts;

    using Linn.Common.Facade;
    using Linn.Stores.Resources;
    using Linn.Stores.Service.Models;

    using Microsoft.Win32.SafeHandles;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class PartsModule : NancyModule
    {
        private readonly IFacadeService<Part, int, PartResource, PartResource> partsFacadeService;

        public PartsModule(IFacadeService<Part, int, PartResource, PartResource> partsFacadeService)
        {
            this.partsFacadeService = partsFacadeService;
            this.Get("/parts/{id}", parameters => this.GetPart(parameters.id));
            this.Get("/parts", _ => this.GetParts());
        }

        private object GetPart(int id)
        {
            var results = this.partsFacadeService.GetById(id);
            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetParts()
        {
            var resource = this.Bind<SearchRequestResource>();
            var results = string.IsNullOrEmpty(resource.SearchTerm)
                              ? this.partsFacadeService.GetAll()
                              : this.partsFacadeService.Search(resource.SearchTerm);
            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}