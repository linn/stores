﻿namespace Linn.Stores.Service.Modules
{
    using System;
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Service.Models;
    using Nancy;
    using Nancy.ModelBinding;

    public sealed class ParcelsModule : NancyModule
    {
        private readonly IParcelService parcelsFacadeService;

        public ParcelsModule(IParcelService parcelsFacadeService)
        {
            this.parcelsFacadeService = parcelsFacadeService;
            this.Get("/inventory/parcels/create", _ => this.Negotiate.WithModel(ApplicationSettings.Get()).WithMediaRangeModel("text/html", ApplicationSettings.Get).WithView("Index"));
            //this.Get("/inventory/parcels", _ => this.Negotiate.WithModel(ApplicationSettings.Get()).WithView("Index"));
            this.Get("/inventory/parcels/{id}", parameters => this.GetParcel(parameters.id));
            this.Put("/inventory/parcels/{id}", parameters => this.UpdateParcel(parameters.id));
            this.Get("/inventory/parcels", _ => this.GetParcels());
            this.Post("/inventory/parcels", _ => this.AddParcel());
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
            var resource = this.Bind<ParcelSearchRequestResource>();

            var results = this.parcelsFacadeService.Search(resource);

            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object AddParcel()
        {
            var resource = this.Bind<ParcelResource>();

            try
            {
                var result = this.parcelsFacadeService.Add(resource);
                return this.Negotiate.WithModel(result).WithMediaRangeModel("text/html", ApplicationSettings.Get);
            }
            catch (Exception e)
            {
                return new ServerFailureResult<Parcel>($"Error when creating - ${(e.InnerException != null ? e.InnerException.Message : e.Message)}");
            }
        }

        private object UpdateParcel(int id)
        {
            var resource = this.Bind<ParcelResource>();

            try
            {
                var result = this.parcelsFacadeService.Update(id, resource);

                return this.Negotiate.WithModel(result)
                    .WithMediaRangeModel("text/html", ApplicationSettings.Get);

            }
            catch (Exception e)
            {
                return new ServerFailureResult<Parcel>($"Error when updating - ${(e.InnerException != null ? e.InnerException.Message : e.Message)}");
            }
        }
    }
}
