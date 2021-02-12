namespace Linn.Stores.Facade.Services
{
    using System.Collections.Generic;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;

    public class CarriersService : ICarriersService
    {
        private readonly IRepository<Carrier, string> repository;

        public CarriersService(IRepository<Carrier, string> repository)
        {
            this.repository = repository;
        }

        public IResult<IEnumerable<Carrier>> SearchCarriers(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return new SuccessResult<IEnumerable<Carrier>>(this.repository.FindAll());
            }

            searchTerm = searchTerm.ToUpper();

            return new SuccessResult<IEnumerable<Carrier>>(
                this.repository.FilterBy(
                    c => (c.Name.ToUpper().Contains(searchTerm) || c.Name.ToUpper().Equals(searchTerm)
                                                                || c.CarrierCode.ToString().Contains(searchTerm)
                                                                || c.CarrierCode.ToString().Equals(searchTerm))));
        }
    }
}
