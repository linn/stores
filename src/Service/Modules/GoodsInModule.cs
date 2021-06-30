namespace Linn.Stores.Service.Modules
{
    using System.Collections.Generic;

    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.RequestResources;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class GoodsInModule : NancyModule
    {
        private readonly IGoodsInFacadeService service;

        public GoodsInModule(IGoodsInFacadeService service)
        {
            this.service = service;
            this.Post("/logistics/book-in", _ => this.DoBookIn());
        }

        private object DoBookIn()
        {
            var resource = this.Bind<BookInRequestResource>();
            var result = this.service.DoBookIn(resource);
            return this.Negotiate.WithModel(this.service.DoBookIn(resource));
        }

        private object GetLoanDetails()
        {
            var resource = this.Bind<LoanDetailResource>();
            var result = this.service.GetLoanDetails(resource.LoanNumber);
            return this.Negotiate.WithModel(result);
        }
    }
}
