namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    using System.Linq;

    using Linn.Common.Persistence;

    public interface IFilterByWildcardQueryRepository<T> : IQueryRepository<T>
    {
        IQueryable<T> FilterByWildcard(string search);
    }
}
