namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    using System;

    public interface IStoresOoPack
    {
        void DoTpk(int locationId, int palletNumber, DateTime dateTimeStarted, out bool success);
    }
}
