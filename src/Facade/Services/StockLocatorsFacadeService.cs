namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Proxy;
    using Linn.Stores.Resources;

    public class StockLocatorsFacadeService : 
        FacadeFilterService<StockLocator, int, StockLocatorResource, StockLocatorResource, StockLocatorResource>,
        IStockLocatorFacadeService
    {
        private readonly IStockLocatorService domainService;

        private readonly IRepository<StockLocator, int> repository;

        private readonly IDatabaseService databaseService;

        public StockLocatorsFacadeService(
            IRepository<StockLocator, int> repository, 
            ITransactionManager transactionManager,
            IStockLocatorService domainService,
            IDatabaseService databaseService)
            : base(repository, transactionManager)
        {
            this.domainService = domainService;
            this.repository = repository;
            this.databaseService = databaseService;
        }

        public IResult<StockLocator> Delete(int id)
        {
            this.domainService.DeleteStockLocator(this.repository.FindById(id));
            return new SuccessResult<StockLocator>(null);
        }

        protected override StockLocator CreateFromResource(StockLocatorResource resource)
        {
            var toCreate = new StockLocator
                               {
                                   Id = this.databaseService.GetNextVal("LOCA_SEQ"),
                                   LocationId = resource.LocationId,
                                   PalletNumber = resource.PalletNumber,
                                   BatchRef = resource.BatchRef,
                                   StockRotationDate = resource.StockRotationDate != null ?
                                                           DateTime.Parse(resource.StockRotationDate) 
                                                           : (DateTime?)null,
                                   Quantity = resource.Quantity,
                                   Remarks = resource.Remarks
                               };

            return this.domainService.CreateStockLocator(toCreate, resource.AuditDepartmentCode);
        }

        protected override void UpdateFromResource(
            StockLocator entity, 
            StockLocatorResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<StockLocator, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<StockLocator, bool>> FilterExpression(StockLocatorResource filterResource)
        {
            return x => x.PartNumber == filterResource.PartNumber;
        }
    }
}
