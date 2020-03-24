namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class UnitsOfMeasureService : IUnitsOfMeasureService
    {
        private readonly IQueryRepository<UnitOfMeasure> repository;

        public UnitsOfMeasureService(IQueryRepository<UnitOfMeasure> repository)
        {
            this.repository = repository;
        }

        public IResult<IEnumerable<UnitOfMeasure>> GetUnitsOfMeasure()
        {
            return new SuccessResult<IEnumerable<UnitOfMeasure>>(
                this.repository.FindAll());
        }
    }
}