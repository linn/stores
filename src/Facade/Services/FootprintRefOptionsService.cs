namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class FootprintRefOptionsService : IFootprintRefOptionsService
    {
        private readonly IQueryRepository<FootprintRefOption> repository;

        public FootprintRefOptionsService(IQueryRepository<FootprintRefOption> repository)
        {
            this.repository = repository;
        }

        public IResult<IEnumerable<FootprintRefOption>> GetOptions()
        {
            return new SuccessResult<IEnumerable<FootprintRefOption>>(
                this.repository.FindAll());
        }
    }
}
