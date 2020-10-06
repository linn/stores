namespace Linn.Stores.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    using Nancy;

    public sealed class CountriesModule : NancyModule
    {
        private readonly IFacadeService<Country, string, CountryResource, CountryResource> countryFacadeService;

        public CountriesModule(IFacadeService<Country, string, CountryResource, CountryResource> countryFacadeService)
        {
            this.countryFacadeService = countryFacadeService;
            this.Get("/logistics/countries", _ => this.GetCountries());
        }

        private object GetCountries()
        {
            return this.Negotiate.WithModel(this.countryFacadeService.GetAll());
        }
    }
}