namespace Linn.Stores.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.Parts;

    public interface IPartLiveService
    {
        IResult<PartLiveTest> CheckIfPartCanBeMadeLive(int id);
    }
}
