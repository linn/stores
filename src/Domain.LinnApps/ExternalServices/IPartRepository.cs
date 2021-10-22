namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;

    public interface IPartRepository : IRepository<Part, int>
    {
        IEnumerable<Part> SearchParts(string searchTerm, int? resultLimit);
    }
}
