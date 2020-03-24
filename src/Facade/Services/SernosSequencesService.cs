namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class SernosSequencesService : ISernosSequencesService
    {
        private readonly IQueryRepository<SernosSequence> repository;

        public SernosSequencesService(IQueryRepository<SernosSequence> repository)
        {
            this.repository = repository;
        }

        public IResult<IEnumerable<SernosSequence>> GetSequences()
        {
            return new SuccessResult<IEnumerable<SernosSequence>>(
                this.repository.FindAll());
        }
    }
}