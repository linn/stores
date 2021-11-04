namespace Linn.Stores.Service.Modules
{
    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Service.Extensions;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class ParcelsModule : NancyModule
    {
        private readonly IAuthorisationService authorisationService;

        private readonly IFacadeFilterService<Parcel, int, ParcelResource, ParcelResource, ParcelSearchRequestResource> parcelsFacadeService;

        public ParcelsModule(
            IFacadeFilterService<Parcel, int, ParcelResource, ParcelResource, ParcelSearchRequestResource> parcelsFacadeService,
            IAuthorisationService authorisationService)
        {
            this.parcelsFacadeService = parcelsFacadeService;
            this.authorisationService = authorisationService;

            this.Get(
                "/logistics/parcels/create",
                _ => this.Negotiate.WithModel(ApplicationSettings.Get()).WithView("Index"));
            this.Get("/logistics/parcels/{id}", parameters => this.GetParcel(parameters.id));
            this.Put("/logistics/parcels/{id}", parameters => this.UpdateParcel(parameters.id));
            this.Get("/logistics/parcels", _ => this.GetParcels());
            this.Get("/logistics/parcels-by-number", _ => this.GetParcelsByNumber());
            this.Post("/logistics/parcels", _ => this.AddParcel());
        }

        private object AddParcel()
        {
            if (!this.authorisationService.HasPermissionFor(
                    AuthorisedAction.ParcelAdmin,
                    this.Context.CurrentUser.GetPrivileges()))
            {
                return new UnauthorisedResult<Parcel>("You are not authorised to create parcels");
            }

            var resource = this.Bind<ParcelResource>();

            var result = this.parcelsFacadeService.Add(resource);

            return this.Negotiate.WithModel(result).WithMediaRangeModel("text/html", ApplicationSettings.Get);
        }

        private object GetParcel(int id)
        {
            var result = this.parcelsFacadeService.GetById(id);

            return this.Negotiate.WithModel(result).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetParcels()
        {
            var resource = this.Bind<ParcelSearchRequestResource>();

            var results = this.parcelsFacadeService.FilterBy(resource);

            return this.Negotiate.WithModel(results).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetParcelsByNumber()
        {
            var resource = this.Bind<SearchRequestResource>();

            var results = this.parcelsFacadeService.Search(resource.SearchTerm);

            return this.Negotiate.WithModel(results).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object UpdateParcel(int id)
        {
            if (!this.authorisationService.HasPermissionFor(
                    AuthorisedAction.ParcelAdmin,
                    this.Context.CurrentUser.GetPrivileges()))
            {
                return new UnauthorisedResult<Parcel>("You are not authorised to update parcels");
            }

            var resource = this.Bind<ParcelResource>();

            if (!string.IsNullOrWhiteSpace(resource.DateCancelled) && !this.authorisationService.HasPermissionFor(
                    AuthorisedAction.ParcelKillAdmin,
                    this.Context.CurrentUser.GetPrivileges()))
            {
                return new UnauthorisedResult<Parcel>("You are not authorised to kill parcels");
            }

            var result = this.parcelsFacadeService.Update(id, resource);

            return this.Negotiate.WithModel(result).WithMediaRangeModel("text/html", ApplicationSettings.Get);
        }
    }
}
