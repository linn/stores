namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq.Expressions;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Common.Proxy.LinnApps;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.RequestResources;

    public class ParcelFacadeService : FacadeFilterService<Parcel, int, ParcelResource, ParcelResource, ParcelSearchRequestResource>
    {
        private readonly IDatabaseService databaseService;

        public ParcelFacadeService(
            IRepository<Parcel, int> parcelRepository,
            ITransactionManager transactionManager,
            IDatabaseService databaseService)
            : base(parcelRepository, transactionManager)
        {
            this.databaseService = databaseService;
        }

        protected override Parcel CreateFromResource(ParcelResource resource)
        {
            var parcel = new Parcel
            {
                ParcelNumber = this.databaseService.GetNextVal("PARCEL_SEQ"),
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
                Weight = resource.Weight
            };

            return parcel;
        }

        protected override void UpdateFromResource(Parcel entity, ParcelResource updateResource)
        {
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
            entity.DateCancelled = string.IsNullOrWhiteSpace(updateResource.DateCancelled)
                                       ? (DateTime?)null
                                       : DateTime.Parse(updateResource.DateCancelled);
        }

        protected override Expression<Func<Parcel, bool>> SearchExpression(string searchTerm)
        {
            return x => x.ParcelNumber.ToString().Equals(searchTerm) || x.ParcelNumber.ToString().Contains(searchTerm);
        }

        protected override Expression<Func<Parcel, bool>> FilterExpression(ParcelSearchRequestResource searchTerms)
        {
            return x =>
                (string.IsNullOrWhiteSpace(searchTerms.ParcelNumberSearchTerm)
                 || x.ParcelNumber.ToString().Contains(searchTerms.ParcelNumberSearchTerm))
                && (string.IsNullOrWhiteSpace(searchTerms.SupplierIdSearchTerm)
                    || (x.SupplierId.HasValue && x.SupplierId.ToString().Contains(searchTerms.SupplierIdSearchTerm)))
                && (string.IsNullOrWhiteSpace(searchTerms.CarrierIdSearchTerm)
                    || (x.CarrierId.HasValue && x.CarrierId.ToString().Contains(searchTerms.CarrierIdSearchTerm)))
                && (string.IsNullOrWhiteSpace(searchTerms.ConsignmentNoSearchTerm)
                    || x.ConsignmentNo.Contains(searchTerms.ConsignmentNoSearchTerm))
                && (string.IsNullOrWhiteSpace(searchTerms.SupplierInvNoSearchTerm)
                    || x.SupplierInvoiceNo.Contains(searchTerms.SupplierInvNoSearchTerm))
                && (string.IsNullOrWhiteSpace(searchTerms.CommentsSearchTerm)
                    || x.Comments.Contains(searchTerms.CommentsSearchTerm))
                && (string.IsNullOrWhiteSpace(searchTerms.DateCreatedSearchTerm)
                    || (DateTime.Parse(searchTerms.DateCreatedSearchTerm).AddDays(-1) < x.DateCreated
                        && x.DateCreated < DateTime.Parse(searchTerms.DateCreatedSearchTerm).AddDays(1)))
                && (searchTerms.NoImportBooksAttachedOnly == null || searchTerms.NoImportBooksAttachedOnly == false
                                                                  || x.Impbooks == null || x.Impbooks.Count == 0);
        }
    }
}
