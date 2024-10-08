﻿namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Domain.Exceptions;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Common.Proxy.LinnApps;
    using Linn.Stores.Domain.LinnApps.Exceptions;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Resources.RequestResources;
    using Linn.Stores.Resources.StockLocators;

    public class StockLocatorsFacadeService : 
        FacadeService<StockLocator, int, StockLocatorResource, StockLocatorResource>,
        IStockLocatorFacadeService
    {
        private readonly IStockLocatorService domainService;

        private readonly IFilterByWildcardRepository<StockLocator, int> repository;

        private readonly IDatabaseService databaseService;

        public StockLocatorsFacadeService(
            IFilterByWildcardRepository<StockLocator, int> repository, 
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
            try
            {
                this.domainService.DeleteStockLocator(toDelete, resource.UserPrivileges);
            }
            catch (StockLocatorException ex)
            {
                return new BadRequestResult<StockLocator>(ex.Message);
            }

            return new SuccessResult<StockLocator>(toDelete);
        }

        public IResult<IEnumerable<StockLocatorWithStoragePlaceInfo>> 
            GetStockLocatorsForPart(int partId)
        {
            return new SuccessResult<IEnumerable<StockLocatorWithStoragePlaceInfo>>(
                this.domainService.GetStockLocatorsWithStoragePlaceInfoForPart(partId));
        }

        public IResult<IEnumerable<StockLocator>> GetBatches(string batchRef)
        {
            return new SuccessResult<IEnumerable<StockLocator>>(
                this.domainService.GetBatches(batchRef));
        }

        public IResult<IEnumerable<StockLocator>> GetStockLocations(StockLocatorQueryResource searchResource)
        {
            try
            {
                if (searchResource.QueryBatchView)
                {
                    return new SuccessResult<IEnumerable<StockLocator>>(this.domainService.SearchStockLocatorBatchView(
                        searchResource.PartNumber,
                        searchResource.LocationId,
                        searchResource.PalletNumber,
                        searchResource.StockPoolCode,
                        searchResource.State,
                        searchResource.Category));
                }

                return new SuccessResult<IEnumerable<StockLocator>>(this.domainService.SearchStockLocators(
                    searchResource.PartNumber,
                    searchResource.LocationId,
                    searchResource.PalletNumber,
                    searchResource.StockPoolCode,
                    searchResource.State,
                    searchResource.Category,
                    searchResource.LocationName,
                    searchResource.PartDescription));
            }
            catch (DomainException x)
            {
                return new ServerFailureResult<IEnumerable<StockLocator>>(x.Message);
            }
        }

        public IResult<IEnumerable<StockMove>> GetMoves(string partNumber, int? palletNumber, int? locationId)
        {
            return new SuccessResult<IEnumerable<StockMove>>(
                this.domainService.GetMoves(partNumber, palletNumber, locationId));
        }

        public IResult<IEnumerable<StockLocator>> GetBatchesInRotationOrderByPart(string partSearch)
        {
            try
            {
                return new SuccessResult<IEnumerable<StockLocator>>(
                    this.domainService.GetBatchesInRotationOrderByPart(partSearch));
            }
            catch (StockLocatorException e)
            {
                return new BadRequestResult<IEnumerable<StockLocator>>(e.Message);
            }
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
