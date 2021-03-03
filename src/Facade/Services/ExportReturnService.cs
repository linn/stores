﻿namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Resources;

    public class ExportReturnService : FacadeService<ExportReturn, int, ExportReturnResource, ExportReturnResource>,
                                       IExportReturnService
    {
        private readonly IQueryRepository<ExportRsn> repository;

        private readonly IRepository<ExportReturn, int> exportReturnRepository;

        private readonly IExportReturnsPack exportReturnsPack;

        public ExportReturnService(
            IQueryRepository<ExportRsn> repository,
            IExportReturnsPack exportReturnsPack,
            IRepository<ExportReturn, int> exportReturnRepository,
            ITransactionManager transactionManager)
            : base(exportReturnRepository, transactionManager)
        {
            this.repository = repository;
            this.exportReturnsPack = exportReturnsPack;
            this.exportReturnRepository = exportReturnRepository;
        }

        public IResult<IEnumerable<ExportRsn>> SearchRsns(int accountId, int? outletNumber)
        {
            if (outletNumber != null)
            {
                return new SuccessResult<IEnumerable<ExportRsn>>(
                    this.repository.FilterBy(rsn => rsn.AccountId == accountId && rsn.OutletNumber == outletNumber));
            }

            return new SuccessResult<IEnumerable<ExportRsn>>(
                this.repository.FilterBy(rsn => rsn.AccountId == accountId));
        }

        public IResult<ExportReturn> MakeExportReturn(IEnumerable<int> rsns, bool hubReturn)
        {
            int exportReturnId;

            try
            {
                exportReturnId = this.exportReturnsPack.MakeExportReturn(string.Join(",", rsns), hubReturn ? "Y" : "N");
            }
            catch (Exception e)
            {
                return new BadRequestResult<ExportReturn>(e.Message);
            }

            var exportReturn = this.exportReturnRepository.FindById(exportReturnId);

            if (exportReturn == null)
            {
                return new BadRequestResult<ExportReturn>("Error creating Export Return");
            }

            return new SuccessResult<ExportReturn>(exportReturn);
        }

        protected override ExportReturn CreateFromResource(ExportReturnResource resource)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(ExportReturn entity, ExportReturnResource updateResource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<ExportReturn, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}