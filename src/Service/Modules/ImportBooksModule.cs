namespace Linn.Stores.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources.ImportBooks;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class ImportBooksModule : NancyModule
    {
        private readonly IImportBookFacadeService importBookFacadeService;

        private readonly IImportBookExchangeRateService importBookExchangeRateService;

        private readonly IFacadeService<ImportBookTransportCode, int, ImportBookTransportCodeResource, ImportBookTransportCodeResource> importBookTransportCodeService;

        private readonly IFacadeService<ImportBookTransactionCode, int, ImportBookTransactionCodeResource, ImportBookTransactionCodeResource> importBookTransactionCodeFacadeService;

        private readonly IFacadeService<ImportBookCpcNumber, int, ImportBookCpcNumberResource, ImportBookCpcNumberResource> importBookCpcNumberFacadeService;

        private readonly IFacadeService<ImportBookDeliveryTerm, string, ImportBookDeliveryTermResource, ImportBookDeliveryTermResource> importBookDeliveryTermFacadeService;

        private readonly IFacadeService<Port, string, PortResource, PortResource> portFacadeService;

        public ImportBooksModule(
            IImportBookFacadeService importBookFacadeService,
            IImportBookExchangeRateService importBookExchangeRateService,
            IFacadeService<ImportBookTransportCode, int, ImportBookTransportCodeResource, ImportBookTransportCodeResource> importBookTransportCodeService,
            IFacadeService<ImportBookTransactionCode, int, ImportBookTransactionCodeResource, ImportBookTransactionCodeResource> importBookTransactionCodeFacadeService,
            IFacadeService<ImportBookCpcNumber, int, ImportBookCpcNumberResource, ImportBookCpcNumberResource> importBookCpcNumberFacadeService,
            IFacadeService<ImportBookDeliveryTerm, string, ImportBookDeliveryTermResource, ImportBookDeliveryTermResource> importBookDeliveryTermFacadeService,
            IFacadeService<Port, string, PortResource, PortResource> portFacadeService)
        {
            this.importBookFacadeService = importBookFacadeService;
            this.importBookExchangeRateService = importBookExchangeRateService;
            this.importBookTransportCodeService = importBookTransportCodeService;
            this.importBookTransactionCodeFacadeService = importBookTransactionCodeFacadeService;
            this.importBookCpcNumberFacadeService = importBookCpcNumberFacadeService;
            this.importBookDeliveryTermFacadeService = importBookDeliveryTermFacadeService;
            this.portFacadeService = portFacadeService;

            this.Get("/logistics/import-books/create", _ => this.Negotiate.WithModel(ApplicationSettings.Get()).WithView("Index"));
            this.Get("/logistics/import-books/{id}", parameters => this.GetImportBook(parameters.id));
            this.Put("/logistics/import-books/{id}", parameters => this.UpdateImportBook(parameters.id));
            this.Post("/logistics/import-books", parameters => this.CreateImportBook());
            this.Get("/logistics/import-books", parameters => this.GetImportBooks());
            this.Get("/logistics/import-books/exchange-rates", parameters => this.GetExchangeRates());
            this.Get("/logistics/import-books/transport-codes", parameters => this.GetTransportCodes());
            this.Get("/logistics/import-books/transaction-codes", parameters => this.GetTransactionCodes());
            this.Get("/logistics/import-books/cpc-numbers", parameters => this.GetCpcNumbers());
            this.Get("/logistics/import-books/ports", parameters => this.GetPorts());
            this.Get("/logistics/import-books/delivery-terms", parameters => this.GetDeliveryTerms());
            this.Post("/logistics/import-books/post-duty", _ => this.PostDuty());
        }

        private object GetImportBook(int id)
        {
            var results = this.importBookFacadeService.GetById(id);
            return this.Negotiate.WithModel(results).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object GetImportBooks()
        {
            var resource = this.Bind<SearchRequestResource>();

            if (string.IsNullOrWhiteSpace(resource.SearchTerm))
            {
                return this.Negotiate.WithMediaRangeModel("text/html", ApplicationSettings.Get)
                    .WithView("Index");
            }

            var results = this.importBookFacadeService.Search(resource.SearchTerm);

            return this.Negotiate.WithModel(results).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object UpdateImportBook(int id)
        {
            var resource = this.Bind<ImportBookResource>();

            var result = this.importBookFacadeService.Update(id, resource);

            return this.Negotiate.WithModel(result).WithMediaRangeModel("text/html", ApplicationSettings.Get);
        }

        private object CreateImportBook()
        {
            var resource = this.Bind<ImportBookResource>();

            var result = this.importBookFacadeService.Add(resource);

            return this.Negotiate.WithModel(result).WithMediaRangeModel("text/html", ApplicationSettings.Get);
        }

        private object PostDuty()
        {
            var resource = this.Bind<PostDutyResource>();

            var result = this.importBookFacadeService.PostDuty(resource);

            return this.Negotiate.WithModel(result).WithMediaRangeModel("text/html", ApplicationSettings.Get);
        }

        private object GetExchangeRates()
        {
            var resource = this.Bind<SearchRequestResource>();

            var results = this.importBookExchangeRateService.GetExchangeRatesForDate(resource.SearchTerm);

            return this.Negotiate.WithModel(results).WithMediaRangeModel("text/html", ApplicationSettings.Get);
        }

        private object GetTransportCodes()
        {
            var results = this.importBookTransportCodeService.GetAll();

            return this.Negotiate.WithModel(results).WithMediaRangeModel("text/html", ApplicationSettings.Get);
        }

        private object GetTransactionCodes()
        {
            var results = this.importBookTransactionCodeFacadeService.GetAll();

            return this.Negotiate.WithModel(results).WithMediaRangeModel("text/html", ApplicationSettings.Get);
        }

        private object GetCpcNumbers()
        {
            var results = this.importBookCpcNumberFacadeService.GetAll();

            return this.Negotiate.WithModel(results).WithMediaRangeModel("text/html", ApplicationSettings.Get);
        }

        private object GetPorts()
        {
            var results = this.portFacadeService.GetAll();

            return this.Negotiate.WithModel(results).WithMediaRangeModel("text/html", ApplicationSettings.Get);
        }

        private object GetDeliveryTerms()
        {
            var results = this.importBookDeliveryTermFacadeService.GetAll();

            return this.Negotiate.WithModel(results).WithMediaRangeModel("text/html", ApplicationSettings.Get);
        }

    }
}
