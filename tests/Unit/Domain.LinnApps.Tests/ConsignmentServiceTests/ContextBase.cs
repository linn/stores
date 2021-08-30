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

        protected IInvoicingPack InvoicingPack { get; private set; }

        protected IExportBookPack ExportBookPack { get; private set; }

        protected IPrintInvoiceDispatcher PrintInvoiceDispatcher { get; private set; }

        protected IPrintConsignmentNoteDispatcher PrintConsignmentNoteDispatcher { get; private set; }

        protected IRepository<PrinterMapping, int> PrinterMappingRepository { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.ConsignmentRepository = Substitute.For<IRepository<Consignment, int>>();
            this.ExportBookRepository = Substitute.For<IRepository<ExportBook, int>>();
            this.ConsignmentProxyService = Substitute.For<IConsignmentProxyService>();
            this.InvoicingPack = Substitute.For<IInvoicingPack>();
            this.ExportBookPack = Substitute.For<IExportBookPack>();
            this.PrintInvoiceDispatcher = Substitute.For<IPrintInvoiceDispatcher>();
            this.PrintConsignmentNoteDispatcher = Substitute.For<IPrintConsignmentNoteDispatcher>();
            this.PrinterMappingRepository = Substitute.For<IRepository<PrinterMapping, int>>();

            this.ConsignmentId = 808;
            this.Consignment = new Consignment
                                   {
                                       ConsignmentId = this.ConsignmentId,
                                       Status = "L",
                                       Address = new Address { Id = 1111, Country = new Country { CountryCode = "GB" } }
                                   };
            this.ConsignmentRepository.FindById(this.ConsignmentId).Returns(
                new Consignment
                    {
                        ConsignmentId = this.ConsignmentId,
                        Invoices = new List<Invoice>
                                       {
                                           new Invoice { DocumentNumber = 123, DocumentType = "I" },
                                           new Invoice { DocumentNumber = 456, DocumentType = "I" }
                                       }
                    });
            this.PrinterMappingRepository.FindBy(Arg.Any<Expression<Func<PrinterMapping, bool>>>())
                .Returns(new PrinterMapping { PrinterName = "Invoice" });

            this.Sut = new ConsignmentService(
                this.ConsignmentRepository,
                this.ExportBookRepository,
                this.ConsignmentProxyService,
                this.InvoicingPack,
                this.ExportBookPack,
                this.PrintInvoiceDispatcher,
                this.PrintConsignmentNoteDispatcher,
                this.PrinterMappingRepository);
        }
    }
}
