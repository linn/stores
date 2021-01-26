namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class PartDataSheetValuesService : IPartDataSheetValuesService
    {
        private readonly IQueryRepository<PartDataSheetValues> repository;

        public PartDataSheetValuesService(IQueryRepository<PartDataSheetValues> repository)
        {
            this.repository = repository;
        }

        public IResult<IEnumerable<PartDataSheetValues>> GetAll()
        {
            return new SuccessResult<IEnumerable<PartDataSheetValues>>(
                this.repository.FindAll());
        }
    }
}
