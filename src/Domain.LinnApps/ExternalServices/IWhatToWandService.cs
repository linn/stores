namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    using System.Collections.Generic;

    using Linn.Stores.Domain.LinnApps.Tpk.Models;

    public interface IWhatToWandService
    {
        IEnumerable<WhatToWandLine> WhatToWand(int? locationId, int? palletNumber);

        bool ShouldPrintWhatToWand(string storagePlace);

        IEnumerable<WhatToWandLine> ReprintWhatToWand(int consignmentId, string country);
    }
}
