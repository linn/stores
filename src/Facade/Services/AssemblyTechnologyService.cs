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

    public class AssemblyTechnologyService : 
        IFacadeService<AssemblyTechnology, string, AssemblyTechnologyResource, AssemblyTechnologyResource>
    {
        private readonly IRepository<AssemblyTechnology, string> repository;

        public AssemblyTechnologyService(IRepository<AssemblyTechnology, string> repository)
        {
            this.repository = repository;
        }

        public IResult<AssemblyTechnology> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public IResult<ResponseModel<AssemblyTechnology>> GetById(string id, IEnumerable<string> privileges)
        {
            throw new NotImplementedException();
        }

        public IResult<IEnumerable<AssemblyTechnology>> GetAll()
        {
            return new SuccessResult<IEnumerable<AssemblyTechnology>>(this.repository.FindAll());
        }

        public IResult<ResponseModel<IEnumerable<AssemblyTechnology>>> GetAll(IEnumerable<string> privileges)
        {
            throw new NotImplementedException();
        }

        public IResult<IEnumerable<AssemblyTechnology>> Search(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public IResult<ResponseModel<IEnumerable<AssemblyTechnology>>> Search(string searchTerm, IEnumerable<string> privileges)
        {
            throw new NotImplementedException();
        }

        public IResult<IPagedList<AssemblyTechnology>> GetAll(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public IResult<ResponseModel<IPagedList<AssemblyTechnology>>> GetAll(int pageNumber, int pageSize, IEnumerable<string> privileges)
        {
            throw new NotImplementedException();
        }

        public IResult<IPagedList<AssemblyTechnology>> GetAll<TKeySort>(int pageNumber, int pageSize, Expression<Func<AssemblyTechnology, TKeySort>> keySelectorForSort, bool asc)
        {
            throw new NotImplementedException();
        }

        public IResult<ResponseModel<IPagedList<AssemblyTechnology>>> GetAll<TKeySort>(int pageNumber, int pageSize, Expression<Func<AssemblyTechnology, TKeySort>> keySelectorForSort, bool asc, IEnumerable<string> privileges)
        {
            throw new NotImplementedException();
        }

        public IResult<AssemblyTechnology> Add(AssemblyTechnologyResource resource)
        {
            throw new NotImplementedException();
        }

        public IResult<ResponseModel<AssemblyTechnology>> Add(AssemblyTechnologyResource resource, IEnumerable<string> privileges)
        {
            throw new NotImplementedException();
        }

        public IResult<AssemblyTechnology> Update(string id, AssemblyTechnologyResource updateResource)
        {
            throw new NotImplementedException();
        }

        public IResult<ResponseModel<AssemblyTechnology>> Update(string id, AssemblyTechnologyResource updateResource, IEnumerable<string> privileges)
        {
            throw new NotImplementedException();
        }
    }
}