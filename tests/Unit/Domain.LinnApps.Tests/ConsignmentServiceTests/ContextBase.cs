namespace Linn.Stores.Domain.LinnApps.Tests.ConsignmentServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Domain.LinnApps.Dispatchers;
    using Linn.Stores.Domain.LinnApps.ExportBooks;
    using Linn.Stores.Domain.LinnApps.ExternalServices;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected IConsignmentService Sut { get; private set; }

        protected IRepository<Consignment, int> ConsignmentRepository { get; private set; }

        protected IRepository<ExportBook, int> ExportBookRepository { get; private set; }

        protected Consignment Consignment { get; set; }

        protected int ConsignmentId { get; set; }

        protected IConsignmentProxyService ConsignmentProxyService { get; private set; }

        protected IPrintService PrintService { get; private set; }

        protected IInvoicingPack InvoicingPack { get; private set; }

        protected IExportBookPack ExportBookPack { get; private set; }

        protected IPrintInvoiceDispatcher PrintInvoiceDispatcher { get; private set; }

        protected IPrintConsignmentNoteDispatcher PrintConsignmentNoteDispatcher { get; private set; }

        protected IRepository<PrinterMapping, int> PrinterMappingRepository { get; private set; }

        protected IRepository<Invoice, int> InvoiceRepository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.ConsignmentRepository = Substitute.For<IRepository<Consignment, int>>();
            this.ExportBookRepository = Substitute.For<IRepository<ExportBook, int>>();
            this.ConsignmentProxyService = Substitute.For<IConsignmentProxyService>();
            this.PrintInvoiceDispatcher = Substitute.For<IPrintInvoiceDispatcher>();
            this.PrintService = Substitute.For<IPrintService>();
            this.InvoicingPack = Substitute.For<IInvoicingPack>();
            this.ExportBookPack = Substitute.For<IExportBookPack>();
            this.PrintInvoiceDispatcher = Substitute.For<IPrintInvoiceDispatcher>();
            this.PrintConsignmentNoteDispatcher = Substitute.For<IPrintConsignmentNoteDispatcher>();
            this.PrinterMappingRepository = Substitute.For<IRepository<PrinterMapping, int>>();
            this.InvoiceRepository = Substitute.For<IRepository<Invoice, int>>();

            this.ConsignmentId = 808;
            this.Consignment = new Consignment
                                   {
                                       ConsignmentId = this.ConsignmentId,
                                       Status = "L",
                                       Address = new Address { Id = 1111, Line1 = "Busy Street", Country = new Country { CountryCode = "GB" } },
                                       Invoices = new List<Invoice>
                                                      {
                                                          new Invoice { DocumentNumber = 123, DocumentType = "I" },
                                                          new Invoice { DocumentNumber = 456, DocumentType = "I" }
                                                      },
                                       Items = new List<ConsignmentItem>
                                                   {
                                                       new ConsignmentItem
                                                           {
                                                               ItemNumber = 1,
                                                               ItemType = "I",
                                                               ItemDescription = "Single Thing",
                                                               Quantity = 1,
                                                               Weight = 2,
                                                               Depth = 50,
                                                               Width = 50,
                                                               Height = 50
                                                           },
                                                       new ConsignmentItem
                                                           {
                                                               ItemNumber = 2,
                                                               ItemType = "C",
                                                               ItemDescription = "Carton",
                                                               ContainerNumber = 1,
                                                               Weight = 1.5m,
                                                               Depth = 50,
                                                               Width = 50,
                                                               Height = 100
                                                           },
                                                       new ConsignmentItem
                                                           {
                                                               ItemNumber = 3,
                                                               ItemType = "I",
                                                               ItemDescription = "Boxed Thing",
                                                               ContainerNumber = 1,
                                                               Quantity = 1
                                                           },
                                                       new ConsignmentItem
                                                           {
                                                               ItemNumber = 4,
                                                               ItemType = "S",
                                                               ItemDescription = "Sealed Thing 1",
                                                               ContainerNumber = 2,
                                                               Quantity = 1,
                                                               Weight = 2,
                                                               Depth = 50,
                                                               Width = 50,
                                                               Height = 50
                                                           },
                                                       new ConsignmentItem
                                                           {
                                                               ItemNumber = 5,
                                                               ItemType = "S",
                                                               ItemDescription = "Sealed Thing 2",
                                                               MaybeHalfAPair = "Y",
                                                               ContainerNumber = 3,
                                                               Quantity = 1,
                                                               PalletNumber = 1,
                                                               Weight = 2,
                                                               Depth = 150,
                                                               Width = 150,
                                                               Height = 150
                                                           }
                                                   },
                                       Pallets = new List<ConsignmentPallet>
                                                     {
                                                         new ConsignmentPallet
                                                             {
                                                                 ConsignmentId = this.ConsignmentId,
                                                                 PalletNumber = 1,
                                                                 Depth = 100,
                                                                 Height = 120,
                                                                 Weight = 14,
                                                                 Width = 120
                                                             }
                                                     }
                                   };
            this.ConsignmentRepository.FindById(this.ConsignmentId).Returns(this.Consignment);
            this.PrinterMappingRepository.FindBy(Arg.Any<Expression<Func<PrinterMapping, bool>>>())
                .Returns(new PrinterMapping { PrinterName = "Invoice" });

            this.Sut = new ConsignmentService(
                this.ConsignmentRepository,
                this.ExportBookRepository,
                this.ConsignmentProxyService,
                this.PrintService,
                this.InvoicingPack,
                this.ExportBookPack,
                this.PrintInvoiceDispatcher,
                this.PrintConsignmentNoteDispatcher,
                this.PrinterMappingRepository,
                this.InvoiceRepository);
        }
    }
}
