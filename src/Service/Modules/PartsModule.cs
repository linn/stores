namespace Linn.Stores.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class PartsModule : NancyModule
    {
        private readonly IFacadeService<Part, int, PartResource, PartResource> partsFacadeService;

        public PartsModule(IFacadeService<Part, int, PartResource, PartResource> partsFacadeService)
        {
            this.partsFacadeService = partsFacadeService;
            this.Get("/parts/create", _ => this.Negotiate.WithModel(ApplicationSettings.Get()).WithView("Index"));
            this.Get("/parts/{id}", parameters => this.GetPart(parameters.id));
            this.Put("/parts/{id}", parameters => this.UpdatePart(parameters.id));
            this.Get("/parts", _ => this.GetParts());
            this.Post("/parts", _ => this.AddPart());
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

        private object AddPart()
        {
            var resource = this.Bind<PartResource>();
            var result = this.partsFacadeService.Add(resource);
            return this.Negotiate.WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get);
        }

        private object UpdatePart(int id)
        {
            var resource = this.Bind<PartResource>();
            var result = this.partsFacadeService.Update(id, resource);
            return this.Negotiate.WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get);
        }
    }
}