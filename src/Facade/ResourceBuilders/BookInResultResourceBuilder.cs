namespace Linn.Stores.Facade.ResourceBuilders
{
    using System;

    using Linn.Common.Facade;
    using Linn.Stores.Domain.LinnApps.GoodsIn;
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
                           KardexLocation = model.KardexLocation
                       };
        }

        public string GetLocation(BookInResult model)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<BookInResult>.Build(BookInResult model) => this.Build(model);
    }
}
