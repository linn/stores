namespace Linn.Stores.Proxy
{
    using System;

    using Linn.Stores.Domain.LinnApps.ExternalServices;

    public class StoresOoPack : IStoresOoPack
    {
        public void DoTpk(int locationId, int palletNumber, DateTime dateTimeStarted, out bool success)
        {
            success = true;
        }
    }
}
