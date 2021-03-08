namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    using System.Collections.Generic;

    using Linn.Stores.Domain.LinnApps.Tpk.Models;

    public interface IWhatToWandService
    {
        IEnumerable<WhatToWandLine> WhatToWand(string fromLocation);
    }
}
