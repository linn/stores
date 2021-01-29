namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Proxy;
    using Linn.Stores.Resources;

    public class StockLocatorsFacadeService : 
        FacadeService<StockLocator, int, StockLocatorResource, StockLocatorResource>,
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
            var toDelete = this.repository.FindById(id);
            this.domainService.DeleteStockLocator(toDelete);
            return new SuccessResult<StockLocator>(toDelete);
        }

        public IResult<IEnumerable<StockLocatorWithStoragePlaceInfo>> 
            GetStockLocatorsForPart(string partNumber)
        {
            return new SuccessResult<IEnumerable<StockLocatorWithStoragePlaceInfo>>(
                this.domainService.GetStockLocatorsWithStoragePlaceInfoForPart(partNumber));
        }

        public IResult<IEnumerable<StockLocator>> FilterBy(StockLocatorResource searchResource)
        {
            throw new NotImplementedException();
        }

        public IResult<ResponseModel<IEnumerable<StockLocator>>> FilterBy(StockLocatorResource searchResource, IEnumerable<string> privileges)
        {
            throw new NotImplementedException();
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
                                   Remarks = resource.Remarks,
                                   PartNumber = resource.PartNumber
                               };

            return this.domainService.CreateStockLocator(toCreate, resource.AuditDepartmentCode);
        }

        protected override void UpdateFromResource(
            StockLocator entity, 
            StockLocatorResource updateResource)
        {
            entity.BatchRef = updateResource.BatchRef;
            entity.StockRotationDate = updateResource.StockRotationDate == null
                                           ? (DateTime?)null
                                           : DateTime.Parse(updateResource.StockRotationDate);
            entity.Quantity = updateResource.Quantity;
            entity.Remarks = updateResource.Remarks;
            entity.PalletNumber = updateResource.PalletNumber;
            entity.LocationId = updateResource.LocationId;
        }

        protected override Expression<Func<StockLocator, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
