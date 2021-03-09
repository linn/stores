namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    using System;

    using Linn.Stores.Domain.LinnApps.Models;

    public interface IStoresPack
    {
        ProcessResult UnAllocateRequisition(int reqNumber, int? reqLineNumber, int userNumber);

        void DoTpk(int locationId, int palletNumber, DateTime dateTimeStarted, out bool success);

        string GetErrorMessage();
    }
}
