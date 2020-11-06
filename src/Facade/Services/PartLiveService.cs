namespace Linn.Stores.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class PartLiveService : IPartLiveService
    {
        private readonly IPartPack partPack;

        private readonly IRepository<Part, int> partRepository;

        public PartLiveService(IPartPack partPack, IRepository<Part, int> partRepository)
        {
            this.partPack = partPack;
            this.partRepository = partRepository;
        }

        public IResult<PartLiveTest> CheckIfPartCanBeMadeLive(int id)
        {
            var partNumber = this.partRepository.FindBy(p => p.Id == id)?.PartNumber;
            if (partNumber == null)
            {
                return new NotFoundResult<PartLiveTest>("Part Not Found");
            }

            var result = this.partPack.PartLiveTest(partNumber, out var message);
            return new SuccessResult<PartLiveTest>(new PartLiveTest()
                                                                   {
                                                                       Result = result,
                                                                       Message = message
                                                                   });
        }
    }
}
