namespace Linn.Stores.Service.Modules.Purchasing
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Purchasing;
    using Linn.Stores.Resources.Purchasing;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class PurchasingModule : NancyModule
    {
        private readonly IFacadeFilterService<PlCreditDebitNote, int, PlCreditDebitNoteResource, PlCreditDebitNoteResource, PlCreditDebitNoteResource>
            service;
        
        public PurchasingModule(
            IFacadeFilterService<PlCreditDebitNote, int, PlCreditDebitNoteResource, PlCreditDebitNoteResource, PlCreditDebitNoteResource> service)
        {
            this.service = service;
            this.Get("inventory/purchasing/debit-notes", _ => this.GetDebitNotes());
            this.Put("inventory/purchasing/debit-notes/{id}", parameters => this.UpdateDebitNote(parameters.id));
        }

        private object GetDebitNotes()
        {
            var resource = this.Bind<SearchRequestResource>();
            var results = this.service.FilterBy(new PlCreditDebitNoteResource
                                                    {
                                                        SupplierName = resource?.SearchTerm
                                                    });

            return this.Negotiate
                .WithModel(results)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }

        private object UpdateDebitNote(int id)
        {
            // todo - any auth?
            var resource = this.Bind<PlCreditDebitNoteResource>();
            var result = this.service.Update(id, resource);
            return this.Negotiate.WithModel(result)
                .WithMediaRangeModel("text/html", ApplicationSettings.Get);
        }
    }
}
