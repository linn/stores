namespace Linn.Production.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Facade.Services;

    public class CarriersService : ICarriersService
                                  
    {
        private readonly IRepository<Carrier, string> repository;

        public CarriersService(IRepository<Carrier, string> repository)
        {
            this.repository = repository;
        }

        public IResult<IEnumerable<Carrier>> SearchCarriers(string searchTerm)
        {
            try
            {
                return new SuccessResult<IEnumerable<Carrier>>(this.repository.FilterBy(this.SearchExpression(searchTerm)));
            }
            catch (NotImplementedException)
            {
                return new BadRequestResult<IEnumerable<Carrier>>("Search is not implemented");
            }
        }

        private Expression<Func<Carrier, bool>> SearchExpression(string searchTerm)
        {
            return w => !w.DateInvalid.HasValue && (w.Name.ToUpper().Contains(searchTerm.ToUpper()) || w.OrganisationId.ToString().Contains(searchTerm));
        }
    }
}
