﻿namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Proxy;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.RequestResources;

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

        public IResult<StockLocator> Delete(StockLocatorResource resource)
        {
            var toDelete = this.repository.FindById(resource.Id);
            this.domainService.DeleteStockLocator(toDelete, resource.UserPrivileges);
            return new SuccessResult<StockLocator>(toDelete);
        }

        public IResult<IEnumerable<StockLocatorWithStoragePlaceInfo>> 
            GetStockLocatorsForPart(string partNumber)
        {
            return new SuccessResult<IEnumerable<StockLocatorWithStoragePlaceInfo>>(
                this.domainService.GetStockLocatorsWithStoragePlaceInfoForPart(partNumber));
        }

        public IResult<IEnumerable<StockLocator>> GetBatches(string batchRef)
        {
            return new SuccessResult<IEnumerable<StockLocator>>(
                this.domainService.GetBatches(batchRef));
        }

        public IResult<IEnumerable<StockLocator>> GetStockLocations(StockLocatorQueryResource searchResource)
        {
            return new SuccessResult<IEnumerable<StockLocator>>(this.domainService.GetStockLocatorLocationsView(
                searchResource.PartNumber,
                searchResource.LocationId,
                searchResource.PalletNumber,
                searchResource.StockPoolCode,
                searchResource.State,
                searchResource.BatchRef,
                searchResource.QueryBatchView));
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

            return this.domainService.CreateStockLocator(
                toCreate, 
                resource.AuditDepartmentCode, 
                resource.UserPrivileges);
        }

        protected override void UpdateFromResource(
            StockLocator entity, 
            StockLocatorResource updateResource)
        {
            var updated = new StockLocator 
                              {
                                BatchRef = updateResource.BatchRef,
                                StockRotationDate = updateResource.StockRotationDate == null
                                                        ? (DateTime?)null
                                                        : DateTime.Parse(updateResource.StockRotationDate),
                                Quantity = updateResource.Quantity,
                                Remarks = updateResource.Remarks,
                                PalletNumber = updateResource.PalletNumber,
                                LocationId = updateResource.LocationId,
                              };
            this.domainService.UpdateStockLocator(entity, updated, updateResource.UserPrivileges);
        }

        protected override Expression<Func<StockLocator, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
