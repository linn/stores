namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.GoodsIn;

    public class RsnAccessoriesService : IRsnAccessoriesService
    {
        private readonly IQueryRepository<RsnAccessory> repository;

        public RsnAccessoriesService(IQueryRepository<RsnAccessory> repository)
        {
            this.repository = repository;
        }

        public IResult<IEnumerable<RsnAccessory>> GetRsnAccessories()
        {
            return new SuccessResult<IEnumerable<RsnAccessory>>(this.repository.FindAll());
        }
    }
}
