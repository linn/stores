namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.GoodsIn;

    public class RsnConditionsService : IRsnConditionsService
    {
        private readonly IQueryRepository<RsnCondition> repository;

        public RsnConditionsService(IQueryRepository<RsnCondition> repository)
        {
            this.repository = repository;
        }

        public IResult<IEnumerable<RsnCondition>> GetRsnConditions()
        {
            return new SuccessResult<IEnumerable<RsnCondition>>(this.repository.FindAll());
        }
    }
}
