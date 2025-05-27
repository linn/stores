namespace Linn.Stores.Service.Modules
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.RequestResources;
    using Nancy;
    using Nancy.ModelBinding;

    public class StoresTransactionDefinitionModule : NancyModule
    {
        private readonly IFacadeService<StoresTransactionDefinition, string, StoresTransactionDefinitionResource, StoresTransactionDefinitionResource> storesTransactionDefinitionFacadeService;


        public StoresTransactionDefinitionModule(IFacadeService<StoresTransactionDefinition, string, StoresTransactionDefinitionResource, StoresTransactionDefinitionResource> storesTransactionDefinitionFacadeService)
        {   
            this.storesTransactionDefinitionFacadeService = storesTransactionDefinitionFacadeService;
            this.Get("/inventory/stores-transaction-definitions", _ => this.GetStoresTransactionDefinitions());
        }

        private object GetStoresTransactionDefinitions()
        {
            var resource = this.Bind<SearchRequestResource>();

            if (resource?.SearchTerm != null)
            {
                return this.Negotiate.WithModel(this.storesTransactionDefinitionFacadeService.Search(resource.SearchTerm));
            }

            return this.Negotiate.WithModel(this.storesTransactionDefinitionFacadeService.GetAll());
        }
    }
}
