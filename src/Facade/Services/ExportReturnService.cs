﻿namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Resources;

    public class ExportReturnService : FacadeService<ExportReturn, int, ExportReturnResource, ExportReturnResource>,
                                       IExportReturnService
    {
        private readonly IRepository<ExportReturn, int> exportReturnRepository;

        private readonly IExportReturnsPack exportReturnsPack;

        private readonly IRepository<ExportReturnDetail, ExportReturnDetailKey> exportReturnDetailRepository;

        private readonly ITransactionManager transactionManager;

        public ExportReturnService(
            IExportReturnsPack exportReturnsPack,
            IRepository<ExportReturn, int> exportReturnRepository,
            ITransactionManager transactionManager,
            IRepository<ExportReturnDetail, ExportReturnDetailKey> exportReturnDetailRepository)
            : base(exportReturnRepository, transactionManager)
        {
            this.exportReturnsPack = exportReturnsPack;
            this.exportReturnRepository = exportReturnRepository;
            this.transactionManager = transactionManager;
            this.exportReturnDetailRepository = exportReturnDetailRepository;
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

        public IResult<ExportReturn> UpdateExportReturn(int id, ExportReturnResource resource)
        {
            var exportReturn = this.exportReturnRepository.FindById(id);

            if (exportReturn == null)
            {
                return new NotFoundResult<ExportReturn>();
            }

            IEnumerable<ExportReturnDetail> exportReturnDetails = new List<ExportReturnDetail>();

            foreach (var exportReturnDetail in resource.ExportReturnDetails)
            {
                var detail = this.exportReturnDetailRepository.FindById(
                    new ExportReturnDetailKey
                        {
                            ReturnId = exportReturnDetail.ReturnId, RsnNumber = exportReturnDetail.RsnNumber
                        });

                if (detail == null)
                {
                    return new BadRequestResult<ExportReturn>(
                        $"Export Return Detail not found: {exportReturnDetail.ReturnId} - {exportReturnDetail.RsnNumber}");
                }

                this.UpdateExportReturnDetailFromResource(detail, exportReturnDetail);

                exportReturnDetails = exportReturnDetails.Append(detail);
            }

            this.UpdateFromResource(exportReturn, resource);

            exportReturn.ExportReturnDetails = exportReturnDetails;

            this.transactionManager.Commit();

            return new SuccessResult<ExportReturn>(exportReturn);
        }

        public IResult<ExportReturn> MakeIntercompanyInvoices(ExportReturnResource resource)
        {
            string result;

            try
            {
                result = this.exportReturnsPack.MakeIntercompanyInvoices(resource.ReturnId);
            }
            catch (Exception e)
            {
                return new BadRequestResult<ExportReturn>(e.Message);
            }

            if (result != "ok")
            {
                return new BadRequestResult<ExportReturn>(result);
            }

            var exportReturn = this.exportReturnRepository.FindById(resource.ReturnId);

            if (exportReturn == null)
            {
                return new NotFoundResult<ExportReturn>();
            }

            return this.UpdateExportReturn(resource.ReturnId, resource);
        }

        protected override ExportReturn CreateFromResource(ExportReturnResource resource)
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<ExportReturn, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFromResource(ExportReturn entity, ExportReturnResource updateResource)
        {
            entity.CarrierCode = updateResource.CarrierCode;
            entity.Currency = updateResource.Currency;
            entity.AccountId = updateResource.AccountId;
            entity.HubId = updateResource.HubId;
            entity.OutletNumber = updateResource.OutletNumber;
            entity.DateDispatched = updateResource.DateDispatched != null
                                        ? DateTime.Parse(updateResource.DateDispatched)
                                        : (DateTime?)null;
            entity.DateCancelled = updateResource.DateCancelled != null
                                       ? DateTime.Parse(updateResource.DateCancelled)
                                       : (DateTime?)null;
            entity.CarrierRef = updateResource.CarrierRef;
            entity.Terms = updateResource.Terms;
            entity.NumPallets = updateResource.NumPallets;
            entity.NumCartons = updateResource.NumCartons;
            entity.GrossWeightKg = updateResource.GrossWeightKg;
            entity.GrossDimsM3 = updateResource.GrossDimsM3;
            entity.ReturnForCredit = updateResource.ReturnForCredit;
            entity.ExportCustomsEntryCode = updateResource.ExportCustomsEntryCode;
            entity.ExportCustomsCodeDate = updateResource.ExportCustomsEntryDate != null
                                               ? DateTime.Parse(updateResource.ExportCustomsEntryDate)
                                               : (DateTime?)null;
        }

        private void UpdateExportReturnDetailFromResource(
            ExportReturnDetail entity,
            ExportReturnDetailResource updateResource)
        {
            entity.ArticleNumber = updateResource.ArticleNumber;
            entity.LineNo = updateResource.LineNo;
            entity.Qty = updateResource.Qty;
            entity.Description = updateResource.Description;
            entity.CustomsValue = updateResource.CustomsValue;
            entity.BaseCustomsValue = updateResource.BaseCustomsValue;
            entity.TariffId = updateResource.TariffId;
            entity.NumCartons = updateResource.NumCartons;
            entity.Weight = updateResource.Weight;
            entity.Width = updateResource.Width;
            entity.Height = updateResource.Height;
            entity.Depth = updateResource.Depth;
        }
    }
}
