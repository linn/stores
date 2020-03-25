namespace Linn.Production.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Facade.Services;
    using Linn.Stores.Resources;

    public class CarriersService : FacadeService<Carrier, int, CarrierResource, CarrierResource>,
                                    IFacadeWithSearchReturnTen<Carrier, int, CarrierResource, CarrierResource>
    {
        private readonly IRepository<Carrier, int> repository;

        public CarriersService(IRepository<Carrier, int> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
            this.repository = repository;
        }

        public IResult<IEnumerable<Carrier>> SearchReturnTen(string searchTerm)
        {
            try
            {
                return new SuccessResult<IEnumerable<Carrier>>(this.repository.FilterBy(this.SearchExpression(searchTerm)).ToList().Take(10));
            }
            catch (NotImplementedException)
            {
                return new BadRequestResult<IEnumerable<Carrier>>("Search is not implemented");
            }
        }

        protected override Carrier CreateFromResource(CarrierResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(Carrier entity, CarrierResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<Carrier, bool>> SearchExpression(string searchTerm)
        {
            return w => !w.DateInvalid.HasValue && (w.Name.ToUpper().Contains(searchTerm.ToUpper()) || w.OrganisationId.ToString().Contains(searchTerm));
        }
    }
}