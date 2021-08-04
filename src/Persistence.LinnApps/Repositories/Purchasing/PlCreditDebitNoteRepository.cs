namespace Linn.Stores.Persistence.LinnApps.Repositories.Purchasing
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Purchasing;

    using Microsoft.EntityFrameworkCore;

    public class PlCreditDebitNoteRepository : IRepository<PlCreditDebitNote, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public PlCreditDebitNoteRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public PlCreditDebitNote FindById(int key)
        {
            return this.serviceDbContext.PlCreditDebitNotes
                .Where(n => n.NoteNumber == key).ToList().FirstOrDefault();
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
            return this.serviceDbContext.PlCreditDebitNotes.Include(n => n.Supplier)
                .Where(expression);
        }
    }
}
