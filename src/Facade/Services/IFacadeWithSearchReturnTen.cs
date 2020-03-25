namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;

    public interface IFacadeWithSearchReturnTen<T, in TKey, in TResource, in TUpdateResource> : IFacadeService<T, TKey, TResource, TUpdateResource>
    {
        IResult<IEnumerable<T>> SearchReturnTen(string searchTerm);
    }
}