namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    using System.Collections.Generic;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;

    public interface IPartRepository : IRepository<Part, int>
    {
        IEnumerable<Part> SearchParts(string searchTerm, int? resultLimit);

        Part GetByIdWithManufacturerData(int id);
        
        IEnumerable<Part> SearchPartsWithWildcard(
            string partNumberSearchTerm, 
            string descriptionSearchTerm,
            string productAnalysisCodeSearchTerm,
            string manufacturersPartNumber,
            bool newestFirst = false,
            int? limit = null);
    }
}
