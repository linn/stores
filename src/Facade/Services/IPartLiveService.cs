namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;

    public interface IPartLiveService
    {
        IResult<PartLiveTest> CheckIfPartCanBeMadeLive(int id);

        // IResult<PartLiveTest> MakePartLive(int id, int user, List<Priv>)
    }
}