namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Parts;

    public class PartCategoryService : IPartCategoryService
    {
        private readonly IQueryRepository<PartCategory> repository;

        public PartCategoryService(IQueryRepository<PartCategory> repository)
        {
            this.repository = repository;
        }

        public IResult<IEnumerable<PartCategory>> GetCategories()
        {
            return new SuccessResult<IEnumerable<PartCategory>>(
                this.repository.FindAll());
        }
    }
}