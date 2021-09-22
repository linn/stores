namespace Linn.Stores.Service.ResponseProcessors
{
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;
    using Linn.Stores.Domain.LinnApps.GoodsIn;

    public class BookInResultResponseProcessor : JsonResponseProcessor<BookInResult>
    {
        public BookInResultResponseProcessor(IResourceBuilder<BookInResult> resourceBuilder)
            : base(resourceBuilder, "book-in-result", 1)
        {
        }
    }
}
