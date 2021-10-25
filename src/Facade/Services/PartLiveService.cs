namespace Linn.Stores.Facade.Services
{
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class PartLiveService : IPartLiveService
    {
        private readonly IPartPack partPack;

        private readonly IPartRepository partRepository;

        public PartLiveService(IPartPack partPack, IPartRepository partRepository)
        {
            this.partPack = partPack;
            this.partRepository = partRepository;
        }

        public IResult<PartLiveTest> CheckIfPartCanBeMadeLive(int id)
        {
            var part = this.partRepository.FilterBy(p => p.Id == id).ToList().FirstOrDefault();
            var partNumber = part?.PartNumber;
            if (part?.PartNumber == null)
            {
                return new NotFoundResult<PartLiveTest>("Part Not Found");
            }

            var result = this.partPack.PartLiveTest(partNumber, out var message);
            return new SuccessResult<PartLiveTest>(new PartLiveTest
                                                                   {
                                                                       Result = result,
                                                                       Message = message
                                                                   });
        }
    }
}
