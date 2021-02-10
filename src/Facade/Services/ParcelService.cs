namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.RequestResources;

    public class ParcelService : FacadeService<Parcel, int, ParcelResource, ParcelResource>, IParcelService
    {
        private readonly IRepository<Parcel, int> Repository;

        public ParcelService(IRepository<Parcel, int> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
            this.Repository = repository;
        }

        protected override Parcel CreateFromResource(ParcelResource resource)
        {
            var parcel = new Parcel
                             {
                                 ParcelNumber = resource.ParcelNumber,
                                 CarrierId = resource.CarrierId,
                                 CartonCount = resource.CartonCount,
                                 CheckedById = resource.CheckedById,
                                 Comments = resource.Comments,
                                 ConsignmentNo = resource.ConsignmentNo,
                                 DateCreated = DateTime.Parse(resource.DateCreated),
                                 DateReceived = DateTime.Parse(resource.DateReceived),
                                 PalletCount = resource.PalletCount,
                                 SupplierId = resource.SupplierId,
                                 SupplierInvoiceNo = resource.SupplierInvoiceNo,
                                 Weight = resource.Weight,
                                 CancelledBy = resource.CancelledBy,
                                 CancellationReason = resource.CancellationReason,
                                 DateCancelled = DateTime.Parse(resource.DateCancelled)
            };
            return parcel;
        }

        protected override void UpdateFromResource(Parcel entity, ParcelResource updateResource)
        {
            entity.ParcelNumber = updateResource.ParcelNumber;
            entity.CarrierId = updateResource.CarrierId;
            entity.CartonCount = updateResource.CartonCount;
            entity.CheckedById = updateResource.CheckedById;
            entity.Comments = updateResource.Comments;
            entity.ConsignmentNo = updateResource.ConsignmentNo;
            entity.DateCreated = DateTime.Parse(updateResource.DateCreated);
            entity.DateReceived = DateTime.Parse(updateResource.DateReceived);
            entity.PalletCount = updateResource.PalletCount;
            entity.SupplierId = updateResource.SupplierId;
            entity.SupplierInvoiceNo = updateResource.SupplierInvoiceNo;
            entity.Weight = updateResource.Weight;
            entity.CancelledBy = updateResource.CancelledBy;
            entity.CancellationReason = updateResource.CancellationReason;
            entity.DateCancelled = DateTime.Parse(updateResource.DateCancelled);
        }

        protected override Expression<Func<Parcel, bool>> SearchExpression(string searchTerms)
        {
            throw new NotImplementedException();
        }

        protected Expression<Func<Parcel, bool>> SearchExpression(ParcelSearchRequestResource searchTerms)
        {
            return x => (string.IsNullOrWhiteSpace(searchTerms.SearchTerm)
                         || (x.ParcelNumber.ToString().Equals(searchTerms.SearchTerm)
                             || x.ParcelNumber.ToString().Contains(searchTerms.SearchTerm)));
            //&& (string.IsNullOrWhiteSpace(searchTerms.CarrierIdSearchTerm) || (x.CarrierId.HasValue && x.CarrierId.Value.ToString().Contains(searchTerms.CarrierIdSearchTerm)))
            //    || x.SupplierId.HasValue && (x.SupplierId.ToString().Equals(searchTerms.SupplierIdSearchTerm) || x.SupplierId.ToString().Contains(searchTerms.SupplierIdSearchTerm))
            //        || x.SupplierId.HasValue && (x.SupplierId.ToString().Equals(searchTerms.SupplierIdSearchTerm) || x.SupplierId.ToString().Contains(searchTerms.SupplierIdSearchTerm))
            //            || (Convert.ToDateTime(searchTerms.DateCreatedSearchTerm).AddDays(-1) < x.DateCreated && x.DateCreated > Convert.ToDateTime(searchTerms.DateCreatedSearchTerm).AddDays(1))
            //            || x.SupplierInvoiceNo.Equals(searchTerms.SupplierInvNoSearchTerm) || x.SupplierInvoiceNo.Contains(searchTerms.SupplierInvNoSearchTerm)
            //            || x.ConsignmentNo.Equals(searchTerms.ConsignmentNoSearchTerm) || x.ConsignmentNo.Contains(searchTerms.ConsignmentNoSearchTerm)
            //            || x.Comments.Equals(searchTerms.CommentsSearchTerm) || x.Comments.Contains(searchTerms.CommentsSearchTerm);
        }

        public IResult<IEnumerable<Parcel>> Search(ParcelSearchRequestResource resource)
        {
            return new SuccessResult<IEnumerable<Parcel>>(this.Repository.FilterBy(this.SearchExpression(resource)));

        }
    }
}
