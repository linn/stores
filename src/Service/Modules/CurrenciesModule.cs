namespace Linn.Stores.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    using Nancy;

    public sealed class CurrenciesModule : NancyModule
    {
        private readonly IFacadeService<Currency, string, CurrencyResource, CurrencyResource> currencyFacadeService;

        public CurrenciesModule(IFacadeService<Currency, string, CurrencyResource, CurrencyResource> currencyFacadeService)
        {
            this.currencyFacadeService = currencyFacadeService;
            this.Get("/logistics/currencies", _ => this.GetCurrencies());
        }

        private object GetCurrencies()
        {
            return this.Negotiate.WithModel(this.currencyFacadeService.GetAll());
        }
    }
}
