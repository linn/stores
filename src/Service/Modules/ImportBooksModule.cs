namespace Linn.Stores.Service.Modules
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.Parts;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class ImportBooksModule : NancyModule
    {
        private readonly IFacadeService<ImportBook, int, ImportBookResource, ImportBookResource>
            importBookFacadeService;

        public ImportBooksModule(IFacadeService<ImportBook, int, ImportBookResource, ImportBookResource> importBookFacadeService)
        {
            this.importBookFacadeService = importBookFacadeService;
            this.Get("/inventory/import-books/{id}", parameters => this.GetImportBook(parameters.id));
            this.Put("/inventory/import-books/{id}", parameters => this.UpdateImportBook(parameters.id));
            this.Post("/inventory/import-book/", _ => this.CreateImportBook());
            this.Get("/inventory/import-books", parameters => this.GetImportBooks());


        }

        private object GetImportBook(int id)
        {
            var results = this.importBookFacadeService.GetById(id);
            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
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

            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object UpdateImportBook(int id)
        {
            var resource = this.Bind<ImportBookResource>();

            var result = this.importBookFacadeService.Update(id, resource);

            return this.Negotiate.WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get);
        }

        private object CreateImportBook()
        {
            var resource = this.Bind<ImportBookResource>();

            var result = this.importBookFacadeService.Add(resource);

            return this.Negotiate.WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get);
        }
    }
}