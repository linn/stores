namespace Linn.Stores.Facade.Services
{
    using Linn.Stores.Domain.LinnApps.Parts;

    using Linn.Common.Facade;

    public interface IMechPartSourceWithPartInfoService
    {
        IResult<MechPartSourceWithPartInfo> GetMechPartSourceWithPartInfo(string partNumber);
    }
}
