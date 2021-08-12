namespace Linn.Stores.Facade.Services.Purchasing
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Purchasing;
    using Linn.Stores.Resources.Purchasing;

    public class PlCreditDebitNoteService : 
        FacadeFilterService<PlCreditDebitNote, int, PlCreditDebitNoteResource, PlCreditDebitNoteResource, PlCreditDebitNoteResource>
    {
        private readonly IAuthorisationService authService;

        public PlCreditDebitNoteService(IRepository<PlCreditDebitNote, int> repository, ITransactionManager transactionManager, IAuthorisationService authService)
            : base(repository, transactionManager)
        {
            this.authService = authService;
        }

        protected override PlCreditDebitNote CreateFromResource(PlCreditDebitNoteResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(PlCreditDebitNote entity, PlCreditDebitNoteResource updateResource)
        {
            if (!this.authService.HasPermissionFor("pl-credit-debit-note.update", updateResource.UserPrivileges))
            {
                throw new Exception("You are not authorised to update credit/debit notes");
            }

            if (string.IsNullOrEmpty(updateResource.Notes))
            {
                entity.DateClosed = DateTime.Today;
                entity.ClosedBy = updateResource.ClosedBy;
                entity.ReasonClosed = updateResource.ReasonClosed;
            }
            else
            {
                entity.Notes = updateResource.Notes;
            }
        }

        protected override Expression<Func<PlCreditDebitNote, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<PlCreditDebitNote, bool>> FilterExpression(PlCreditDebitNoteResource searchResource)
        {
            return x => x.NoteType == "D" 
                && x.DateClosed == null 
                && (string.IsNullOrEmpty(searchResource.SupplierName)
                    || x.Supplier.Name.Contains(searchResource.SupplierName.ToUpper()));
        }
    }
}
