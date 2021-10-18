namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    using System.Linq;

    using Linn.Common.Persistence;

    public interface IFilterByWildcardRepository<T, TKey> : IRepository<T, TKey>
    {
        IQueryable<T> FilterByWildcard(string search);
    }
}
