namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Domain.LinnApps.Parts;

    using Linn.Common.Facade;
    using Linn.Stores.Resources;

    using PagedList.Core;

    public class PartFacadeService : IFacadeService<Part, int, PartResource, PartResource>
    {
        public IResult<Part> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IResult<ResponseModel<Part>> GetById(int id, IEnumerable<string> privileges)
        {
            throw new NotImplementedException();
        }

        public IResult<IEnumerable<Part>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IResult<ResponseModel<IEnumerable<Part>>> GetAll(IEnumerable<string> privileges)
        {
            throw new NotImplementedException();
        }

        public IResult<IEnumerable<Part>> Search(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public IResult<ResponseModel<IEnumerable<Part>>> Search(string searchTerm, IEnumerable<string> privileges)
        {
            throw new NotImplementedException();
        }

        public IResult<IPagedList<Part>> GetAll(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public IResult<ResponseModel<IPagedList<Part>>> GetAll(int pageNumber, int pageSize, IEnumerable<string> privileges)
        {
            throw new NotImplementedException();
        }

        public IResult<IPagedList<Part>> GetAll<TKeySort>(int pageNumber, int pageSize, Expression<Func<Part, TKeySort>> keySelectorForSort, bool asc)
        {
            throw new NotImplementedException();
        }

        public IResult<ResponseModel<IPagedList<Part>>> GetAll<TKeySort>(int pageNumber, int pageSize, Expression<Func<Part, TKeySort>> keySelectorForSort, bool asc, IEnumerable<string> privileges)
        {
            throw new NotImplementedException();
        }

        public IResult<Part> Add(PartResource resource)
        {
            throw new NotImplementedException();
        }

        public IResult<ResponseModel<Part>> Add(PartResource resource, IEnumerable<string> privileges)
        {
            throw new NotImplementedException();
        }

        public IResult<Part> Update(int id, PartResource updateResource)
        {
            throw new NotImplementedException();
        }

        public IResult<ResponseModel<Part>> Update(int id, PartResource updateResource, IEnumerable<string> privileges)
        {
            throw new NotImplementedException();
        }
    }
}