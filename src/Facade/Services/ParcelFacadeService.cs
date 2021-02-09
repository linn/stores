namespace Linn.Stores.Facade.Services
{
    using System;
    using System.Linq.Expressions;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Resources;

    public class ParcelFacadeService : FacadeService<Parcel, int, ParcelResource, ParcelResource>
    {
        public ParcelFacadeService(IRepository<Parcel, int> repository, ITransactionManager transactionManager)
            : base(repository, transactionManager)
        {
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

        protected override Expression<Func<Parcel, bool>> SearchExpression(string searchTerm)
        {
            return x => x.ParcelNumber.ToString().Equals(searchTerm) || x.ParcelNumber.ToString().Contains(searchTerm);
        }
    }
}
