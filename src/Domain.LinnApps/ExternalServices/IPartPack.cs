﻿namespace Linn.Stores.Domain.LinnApps.ExternalServices
{
    public interface IPartPack
    {
        string PartRoot(string partNumber);

        bool PartLiveTest(string partNumber, out string message);

        string CreatePartFromSourceSheet(int sourceId, int userNumber, out string message);
    }
}
