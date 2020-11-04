namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Resources;

    using PagedList.Core;

    public class DecrementRuleService :
        IFacadeService<DecrementRule, string, DecrementRuleResource, DecrementRuleResource>
    {
        private readonly IRepository<DecrementRule, string> repository;

        public DecrementRuleService(IRepository<DecrementRule, string> repository)
        {
            this.repository = repository;
        }

        public IResult<DecrementRule> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public IResult<ResponseModel<DecrementRule>> GetById(string id, IEnumerable<string> privileges)
        {
            throw new NotImplementedException();
        }

        public IResult<IEnumerable<DecrementRule>> GetAll()
        {
            return new SuccessResult<IEnumerable<DecrementRule>>(this.repository.FindAll());
        }

        public IResult<ResponseModel<IEnumerable<DecrementRule>>> GetAll(IEnumerable<string> privileges)
        {
            throw new NotImplementedException();
        }

        public IResult<IEnumerable<DecrementRule>> Search(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public IResult<ResponseModel<IEnumerable<DecrementRule>>> Search(string searchTerm, IEnumerable<string> privileges)
        {
            throw new NotImplementedException();
        }

        public IResult<IPagedList<DecrementRule>> GetAll(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public IResult<ResponseModel<IPagedList<DecrementRule>>> GetAll(int pageNumber, int pageSize, IEnumerable<string> privileges)
        {
            throw new NotImplementedException();
        }

        public IResult<IPagedList<DecrementRule>> GetAll<TKeySort>(int pageNumber, int pageSize, Expression<Func<DecrementRule, TKeySort>> keySelectorForSort, bool asc)
        {
            throw new NotImplementedException();
        }

        public IResult<ResponseModel<IPagedList<DecrementRule>>> GetAll<TKeySort>(int pageNumber, int pageSize, Expression<Func<DecrementRule, TKeySort>> keySelectorForSort, bool asc, IEnumerable<string> privileges)
        {
            throw new NotImplementedException();
        }

        public IResult<DecrementRule> Add(DecrementRuleResource resource)
        {
            throw new NotImplementedException();
        }

        public IResult<ResponseModel<DecrementRule>> Add(DecrementRuleResource resource, IEnumerable<string> privileges)
        {
            throw new NotImplementedException();
        }

        public IResult<DecrementRule> Update(string id, DecrementRuleResource updateResource)
        {
            throw new NotImplementedException();
        }

        public IResult<ResponseModel<DecrementRule>> Update(string id, DecrementRuleResource updateResource, IEnumerable<string> privileges)
        {
            throw new NotImplementedException();
        }
    }
}
