namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Resources.Consignments;

    public class CartonTypeFacadeService : FacadeService<CartonType, string, CartonTypeResource, CartonTypeResource>
    {
        public CartonTypeFacadeService(IRepository<CartonType, string> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
        }

        protected override CartonType CreateFromResource(CartonTypeResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(CartonType entity, CartonTypeResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<CartonType, bool>> SearchExpression(string searchTerm)
        {
            return cartonType => cartonType.CartonTypeName.ToUpper().Contains(searchTerm.ToUpper())
                                 || cartonType.Description.ToUpper().Contains(searchTerm.ToUpper());
        }
    }
}
