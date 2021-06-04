namespace Linn.Stores.Service.Modules
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources.Parts;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class ImportBooksModule : NancyModule
    {
        private readonly IFacadeService<ImportBook, int, ImportBookResource, ImportBookResource> importBookFacadeService;

        private readonly IImportBookExchangeRateService importBookExchangeRateService;

        private readonly IImportBookTransportCodeService importBookTransportCodeService;

        private readonly IImportBookTransactionCodeService importBookTransactionCodeService;

        public ImportBooksModule(
            IFacadeService<ImportBook, int, ImportBookResource, ImportBookResource> importBookFacadeService,
            IImportBookExchangeRateService importBookExchangeRateService,
            IImportBookTransportCodeService importBookTransportCodeService,
            IImportBookTransactionCodeService importBookTransactionCodeService)
        {
            this.importBookFacadeService = importBookFacadeService;
            this.importBookExchangeRateService = importBookExchangeRateService;
            this.importBookTransportCodeService = importBookTransportCodeService;
            this.importBookTransactionCodeService = importBookTransactionCodeService;

            this.Get("/logistics/import-books/{id}", parameters => this.GetImportBook(parameters.id));
            this.Put("/logistics/import-books/{id}", parameters => this.UpdateImportBook(parameters.id));
            this.Post("/logistics/import-books/", _ => this.CreateImportBook());
            this.Get("/logistics/import-books", parameters => this.GetImportBooks());
            this.Get("/logistics/import-books/exchange-rates", parameters => this.GetExchangeRates());
            this.Get("/logistics/import-books/transport-codes", parameters => this.GetTransportCodes());
            this.Get("/logistics/import-books/transaction-codes", parameters => this.GetTransactionCodes());
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
                return new BadRequestResult<IEnumerable<ImportBook>>("Search term cannot be empty");
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

        private object GetExchangeRates()
        {
            var resource = this.Bind<SearchRequestResource>();

            var results = this.importBookExchangeRateService.GetExchangeRatesForDate(resource.SearchTerm);

            return this.Negotiate.WithModel(results).WithMediaRangeModel("text/html", ApplicationSettings.Get);
        }

        private object GetTransportCodes()
        {
            var results = this.importBookTransportCodeService.GetTransportCodes();

            return this.Negotiate.WithModel(results).WithMediaRangeModel("text/html", ApplicationSettings.Get);
        }

        private object GetTransactionCodes()
        {
            var results = this.importBookTransactionCodeService.GetTransactionCodes();

            return this.Negotiate.WithModel(results).WithMediaRangeModel("text/html", ApplicationSettings.Get);
        }
    }
}
