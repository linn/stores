namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;
    using System.Linq;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.GoodsIn;
    using Linn.Stores.Resources;
    using Linn.Stores.Resources.GoodsIn;

    public class BookInResultResourceBuilder : IResourceBuilder<BookInResult>
    {
        public BookInResultResource Build(BookInResult model)
        {
            return new BookInResultResource
                       {
                           Success = model.Success,
                           Message = model.Message,
                           TransactionCode = model.TransactionCode,
                           DocType = model.DocType,
                           QcInfo = model.QcInfo,
                           QcState = model.QcState,
                           ReqNumber = model.ReqNumber,
                           QtyReceived = model.QtyReceived,
                           UnitOfMeasure = model.UnitOfMeasure,
                           PartNumber = model.PartNumber,
                           Description = model.PartDescription,
                           KardexLocation = model.KardexLocation,
                           CreateParcel = model.CreateParcel,
                           ParcelComments = model.ParcelComments,
                           SupplierId = model.SupplierId,
                           CreatedBy = model.CreatedBy,
                           Lines = model.Lines?.Select(
                               l => new GoodsInLogEntryResource
                                        {
                                            ArticleNumber = l.ArticleNumber,
                                            DateCreated = l.DateCreated.ToShortDateString(),
                                            OrderLine = l.OrderLine,
                                            OrderNumber = l.OrderNumber,
                                            LoanNumber = l.LoanNumber,
                                            BookInRef = l.BookInRef,
                                            Comments = l.Comments,
                                            CreatedBy = l.CreatedBy,
                                            DemLocation = l.DemLocation,
                                            Id = l.Id,
                                            Quantity = l.Quantity,
                                            ManufacturersPartNumber = l.ManufacturersPartNumber,
                                            State = l.State,
                                            LoanLine = l.LoanLine,
                                            LogCondition = l.LogCondition,
                                            RsnAccessories = l.RsnAccessories,
                                            RsnNumber = l.RsnNumber,
                                            SerialNumber = l.SerialNumber,
                                            StoragePlace = l.StoragePlace,
                                            StorageType = l.StorageType,
                                            TransactionType = l.TransactionType,
                                            WandString = l.WandString
                                        })
                       };
        }

        public string GetLocation(BookInResult model)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<BookInResult>.Build(BookInResult model) => this.Build(model);
    }
}
