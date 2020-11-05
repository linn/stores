namespace Linn.Stores.Persistence.LinnApps.Repositories
{
    using Linn.Stores.Domain.LinnApps.Parts;

    public interface IMechPartSourceWithPartInfoRepository
    {
        MechPartSourceWithPartInfo FindByPartNumber(string partNumber);
    }
}
