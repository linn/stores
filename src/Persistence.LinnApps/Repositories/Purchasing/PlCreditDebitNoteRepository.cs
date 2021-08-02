namespace Linn.Stores.Persistence.LinnApps.Repositories.Purchasing
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Purchasing;

    public class PlCreditDebitNoteRepository : IRepository<PlCreditDebitNote, int>
    {
        public PlCreditDebitNote FindById(int key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<PlCreditDebitNote> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(PlCreditDebitNote entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(PlCreditDebitNote entity)
        {
            throw new NotImplementedException();
        }

        public PlCreditDebitNote FindBy(Expression<Func<PlCreditDebitNote, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<PlCreditDebitNote> FilterBy(Expression<Func<PlCreditDebitNote, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
