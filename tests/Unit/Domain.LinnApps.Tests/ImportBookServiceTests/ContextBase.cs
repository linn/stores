namespace Linn.Stores.Domain.LinnApps.Tests.ImportBookServiceTests
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.ImportBooks;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected IRepository<ImportBookExchangeRate, ImportBookExchangeRateKey> ExchangeRateRepository;

        protected IRepository<LedgerPeriod, int> LedgerPeriodRepository;

        protected IPurchaseLedgerPack PurchaseLedgerPack;

        protected IRepository<Supplier, int> SupplierRepository;

        protected IRepository<ImportBookInvoiceDetail, ImportBookInvoiceDetailKey> InvoiceDetailRepository
        {
            get;
            private set;
        }

        protected IRepository<ImportBookOrderDetail, ImportBookOrderDetailKey> OrderDetailRepository
        {
            get;
            private set;
        }

        protected IRepository<ImportBookPostEntry, ImportBookPostEntryKey> PostEntryRepository { get; private set; }

        protected IImportBookService Sut { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.InvoiceDetailRepository =
                Substitute.For<IRepository<ImportBookInvoiceDetail, ImportBookInvoiceDetailKey>>();
            this.OrderDetailRepository = Substitute.For<IRepository<ImportBookOrderDetail, ImportBookOrderDetailKey>>();
            this.PostEntryRepository = Substitute.For<IRepository<ImportBookPostEntry, ImportBookPostEntryKey>>();
            this.ExchangeRateRepository = Substitute.For<IRepository<ImportBookExchangeRate, ImportBookExchangeRateKey>>();
            this.SupplierRepository = Substitute.For<IRepository<Supplier, int>>();
            this.LedgerPeriodRepository = Substitute.For<IRepository<LedgerPeriod, int>>();
            this.PurchaseLedgerPack = Substitute.For<IPurchaseLedgerPack>();
            this.TransactionManager = Substitute.For<ITransactionManager>();

            this.Sut = new ImportBookService(
                this.ExchangeRateRepository,
                this.LedgerPeriodRepository,
                this.SupplierRepository,
                this.OrderDetailRepository,
                this.TransactionManager,
                this.PurchaseLedgerPack);
        }
    }
}
