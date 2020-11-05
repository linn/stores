namespace Linn.Stores.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;
    using Linn.Stores.Persistence.LinnApps.Repositories;

    public class MechPartSourceWithPartInfoService : IMechPartSourceWithPartInfoService
    {
        private readonly IMechPartSourceWithPartInfoRepository mechPartSourceWithPartInfoRepository;
        
        public MechPartSourceWithPartInfoService(IMechPartSourceWithPartInfoRepository mechPartSourceWithPartInfoRepository)
        {
            this.mechPartSourceWithPartInfoRepository = mechPartSourceWithPartInfoRepository;
        }

        public IResult<MechPartSourceWithPartInfo> GetMechPartSourceWithPartInfo(string partNumber)
        {
            var result = this.mechPartSourceWithPartInfoRepository.FindByPartNumber(partNumber);

            if (result == null)
            {
                return new NotFoundResult<MechPartSourceWithPartInfo>(
                    $"No Mech Part Source Found for {partNumber}");
            }

            return new SuccessResult<MechPartSourceWithPartInfo>(result);
        }
    }
}
