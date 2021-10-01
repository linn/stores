namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    using System.Collections.Generic;

    using Linn.Stores.Domain.LinnApps.Parts;

    public interface IDeptStockPartsService
    {
        IEnumerable<Part> GetDeptStockPalletParts();
    }
}
