namespace Linn.Stores.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Resources.Parts;
    using Linn.Stores.Service.Models;

    using Nancy;

    public sealed class ImportBooksModule : NancyModule
    {
        private readonly IFacadeService<ImportBook, int, ImportBookResource, ImportBookResource>
            importBookFacadeService;

        public ImportBooksModule(IFacadeService<ImportBook, int, ImportBookResource, ImportBookResource> importBookFacadeService)
        {
            this.importBookFacadeService = importBookFacadeService;
            this.Get("/inventory/import-books/{id}", parameters => this.GetImportBook(parameters.id));
        }

        private object GetImportBook(int id)
        {
            var results = this.importBookFacadeService.GetById(id);
            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}