﻿namespace Linn.Stores.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Service.Models;
    using Nancy;
    using Nancy.ModelBinding;

    public sealed class ParcelsModule : NancyModule
    {
        private readonly IFacadeService<Parcel, int, ParcelResource, ParcelResource> parcelsFacadeService;

        public ParcelsModule(IFacadeService<Parcel, int, ParcelResource, ParcelResource> parcelsFacadeService)
        {
            this.parcelsFacadeService = parcelsFacadeService;
            this.Get("/logistics/parcels", _ => this.Negotiate.WithModel(ApplicationSettings.Get()).WithView("Index"));
            this.Get("/logistics/parcels/{id}", parameters => this.GetParcel(parameters.id));
            this.Put("/logistics/parcels/{id}", parameters => this.UpdateParcel(parameters.id));
            this.Get("/logistics/parcels", _ => this.GetParcels());
            this.Post("/logistics/parcels", _ => this.AddParcel());
        }

        private object GetParcel(int id)
        {
            var result = this.parcelsFacadeService.GetById(id);

            return this.Negotiate
                .WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetParcels()
        {
            var resource = this.Bind<SearchRequestResource>();

            var results = string.IsNullOrEmpty(resource.SearchTerm)
                              ? this.parcelsFacadeService.GetAll()
                              : this.parcelsFacadeService.Search(resource.SearchTerm);

return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get);
        }

        private object AddParcel()
        {
            var resource = this.Bind<ParcelResource>();

            var result = this.parcelsFacadeService.Add(resource);

            return this.Negotiate.WithModel(result).WithMediaRangeModel("text/html", ApplicationSettings.Get);
        }

        private object UpdateParcel(int id)
        {
            var resource = this.Bind<ParcelResource>();

            var result = this.parcelsFacadeService.Update(id, resource);

            return this.Negotiate.WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get);
        }
    }
}
