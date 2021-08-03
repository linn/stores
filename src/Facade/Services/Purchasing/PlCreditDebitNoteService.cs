namespace Linn.Stores.Facade.Services.Purchasing
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Resources.Purchasing;

    public class PlCreditDebitNoteService : 
        FacadeFilterService<PlCreditDebitNoteService, int, PlCreditDebitNoteResource, PlCreditDebitNoteResource, PlCreditDebitNoteResource>
    {
        public PlCreditDebitNoteService(IRepository<PlCreditDebitNoteService, int> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override PlCreditDebitNoteService CreateFromResource(PlCreditDebitNoteResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(PlCreditDebitNoteService entity, PlCreditDebitNoteResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<PlCreditDebitNoteService, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<PlCreditDebitNoteService, bool>> FilterExpression(PlCreditDebitNoteResource searchResource)
        {
            throw new NotImplementedException();
        }
    }
}
