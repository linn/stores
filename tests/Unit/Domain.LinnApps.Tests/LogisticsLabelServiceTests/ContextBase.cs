namespace Linn.Stores.Domain.LinnApps.Tests.LogisticsLabelServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Linn.Common.Domain.LinnApps.RemoteServices;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Consignments;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected ILogisticsLabelService Sut { get; private set; }

        protected IBartenderLabelPack BartenderLabelPack { get; private set; }

        protected IRepository<Consignment, int> ConsignmentRepository { get; private set; }

        protected IRepository<PrinterMapping, int> PrinterMappingRepository { get; private set; }

        protected int UserNumber { get; set; }

        protected int ConsignmentId { get; set; }

        protected Consignment Consignment { get; set; }

        [SetUp]
        public void SetUpContext()
        {
            this.BartenderLabelPack = Substitute.For<IBartenderLabelPack>();
            this.ConsignmentRepository = Substitute.For<IRepository<Consignment, int>>();
            this.PrinterMappingRepository = Substitute.For<IRepository<PrinterMapping, int>>();

            this.ConsignmentId = 808;
            this.Consignment = new Consignment
                                   {
                                       ConsignmentId = this.ConsignmentId,
                                       Address =
                                           new Address
                                               {
                                                   Addressee = "Big Shop",
                                                   Line1 = "this",
                                                   Line2 = "address",
                                                   PostCode = "d",
                                                   CountryCode = "FR",
                                                   Country = new Country { CountryCode = "FR", DisplayName = "France" }
                                               },
                                       Pallets = new List<ConsignmentPallet>
                                                     {
                                                         new ConsignmentPallet
                                                             {
                                                                 ConsignmentId = this.ConsignmentId,
                                                                 Depth = 12,
                                                                 Height = 14,
                                                                 PalletNumber = 1,
                                                                 Weight = 16,
                                                                 Width = 18
                                                             }
                                                     },
                                       Items = new List<ConsignmentItem>
                                                   {
                                                       new ConsignmentItem
                                                           {
                                                               ContainerNumber = 2,
                                                               ItemDescription = "Loose Item",
                                                               ConsignmentId = this.ConsignmentId,
                                                               SerialNumber = 123456,
                                                               OrderNumber = 300000,
                                                               OrderLine = 1,
                                                               ItemType = "I",
                                                               ItemNumber = 1
                                                           },
                                                       new ConsignmentItem
                                                           {
                                                               ContainerNumber = 2,
                                                               ItemDescription = "Open Box Carton",
                                                               ConsignmentId = this.ConsignmentId,
                                                               SerialNumber = 123456,
                                                               OrderNumber = 300000,
                                                               OrderLine = 1,
                                                               ItemType = "C",
                                                               ItemNumber = 2
                                                           },
                                                       new ConsignmentItem
                                                           {
                                                               ContainerNumber = 3,
                                                               ItemDescription = "DSM PLAYER 2",
                                                               ConsignmentId = this.ConsignmentId,
                                                               SerialNumber = 536456,
                                                               OrderNumber = 343242,
                                                               OrderLine = 145,
                                                               ItemNumber = 3,
                                                               ItemType = "S"
                                                           }
                                                   }
                                   };

            this.ConsignmentRepository.FindById(this.ConsignmentId).Returns(this.Consignment);
            this.PrinterMappingRepository.FindBy(Arg.Any<Expression<Func<PrinterMapping, bool>>>())
                .Returns(new PrinterMapping
                             {
                                 UserNumber = this.UserNumber,
                                 PrinterGroup = "DISPATCH-LABEL",
                                 PrinterName = "DispatchLabels1"
                             });

            this.Sut = new LogisticsLabelService(
                this.BartenderLabelPack,
                this.ConsignmentRepository,
                this.PrinterMappingRepository);
        }
    }
}
