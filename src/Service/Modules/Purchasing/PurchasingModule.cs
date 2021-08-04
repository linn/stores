namespace Linn.Stores.Service.Modules.Purchasing
{
    using System;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Purchasing;
    using Linn.Stores.Resources.Purchasing;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Service.Extensions;
    using Linn.Stores.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;
    using Nancy.Security;

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
            this.RequiresAuthentication();
            var resource = this.Bind<PlCreditDebitNoteResource>();
            resource.UserPrivileges = this.Context.CurrentUser.GetPrivileges();
            var closedByUri = this.Context.CurrentUser.GetEmployeeUri();
            resource.ClosedBy = int.Parse(closedByUri.Split("/").Last());
            try
            {
                var result = this.service.Update(id, resource);
                return this.Negotiate.WithModel(result)
                    .WithMediaRangeModel("text/html", ApplicationSettings.Get);
            }
            catch (Exception e)
            {
                return new BadRequestResult<PlCreditDebitNote>(e.Message);
            }
        }
    }
}
