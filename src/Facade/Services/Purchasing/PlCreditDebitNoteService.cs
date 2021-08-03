namespace Linn.Stores.Facade.Services.Purchasing
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Purchasing;
    using Linn.Stores.Resources.Purchasing;

    public class PlCreditDebitNoteService : 
        FacadeFilterService<PlCreditDebitNote, int, PlCreditDebitNoteResource, PlCreditDebitNoteResource, PlCreditDebitNoteResource>
    {
        public PlCreditDebitNoteService(IRepository<PlCreditDebitNote, int> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override PlCreditDebitNote CreateFromResource(PlCreditDebitNoteResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(PlCreditDebitNote entity, PlCreditDebitNoteResource updateResource)
        {
            entity.DateClosed = DateTime.Today;
            entity.ClosedBy = updateResource.ClosedBy;
            entity.ReasonClosed = updateResource.ReasonClosed;
        }

        protected override Expression<Func<PlCreditDebitNote, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<PlCreditDebitNote, bool>> FilterExpression(PlCreditDebitNoteResource searchResource)
        {
            return x => x.DateClosed == null && (string.IsNullOrEmpty(searchResource.SupplierName)
                                                 || x.Supplier.Name.Contains(searchResource.SupplierName.ToUpper()));
        }
    }
}
