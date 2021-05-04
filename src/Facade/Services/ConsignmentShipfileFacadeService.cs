namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Persistence.LinnApps.Repositories;

    public class ConsignmentShipfileFacadeService : IConsignmentShipfileFacadeService
    {
        private readonly IQueryRepository<ConsignmentShipfile> repository;

        public ConsignmentShipfileFacadeService(IQueryRepository<ConsignmentShipfile> repository)
        {
            this.repository = repository;
        }

        public IResult<IEnumerable<ConsignmentShipfile>> GetShipfiles()
        {
            return new SuccessResult<IEnumerable<ConsignmentShipfile>>(this.repository.FindAll());
        }
    }
}
