namespace Linn.Stores.Domain.LinnApps.Tests.ImportBookServiceTests
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ImportBooks;
    using Linn.Stores.Domain.LinnApps.Parts;
    using NSubstitute;
    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected IImportBookService Sut { get; private set; }

        protected IRepository<ImportBookInvoiceDetail, ImportBookInvoiceDetailKey> InvoiceDetailRepository { get; private set; }

        protected IRepository<ImportBookOrderDetail, ImportBookOrderDetailKey> OrderDetailRepository { get; private set; }

        protected IRepository<ImportBookPostEntry, ImportBookPostEntryKey> PostEntryRepository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.InvoiceDetailRepository = Substitute.For<IRepository<ImportBookInvoiceDetail, ImportBookInvoiceDetailKey>>();
            this.OrderDetailRepository = Substitute.For<IRepository<ImportBookOrderDetail, ImportBookOrderDetailKey>>();
            this.PostEntryRepository = Substitute.For<IRepository<ImportBookPostEntry, ImportBookPostEntryKey>>();

            this.Sut = new ImportBookService();
        }
    }
}
