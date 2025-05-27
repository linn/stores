namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    using System.Collections.Generic;

    using Linn.Stores.Domain.LinnApps.Scs;

    public interface IScsPalletsRepository 
    {
         void ReplaceAll(
             IEnumerable<ScsStorePallet> pallets);
    }
}
