namespace Linn.Stores.Domain.LinnApps.Tests.MoveStockServiceTests
{
    using System;
    using System.Collections.Generic;

    using FluentAssertions.Extensions;

    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Requisitions;
    using Linn.Stores.Domain.LinnApps.StockMove;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected IMoveStockService Sut { get; private set; }

        protected IStoresPack StoresPack { get; private set; }

        protected IRepository<RequisitionHeader, int> RequisitionRepository { get; private set; }

        protected string PartNumber { get; set; }

        protected int ReqNumber { get; set; }

        protected int Quantity { get; set; }

        protected string From { get; set; }

        protected string To { get; set; }

        protected int UserNumber { get; set; }

        protected DateTime? FromStockDate { get; set; }

        protected RequisitionHeader Req { get; set; }

        [SetUp]
        public void SetUpContext()
        {
            this.StoresPack = Substitute.For<IStoresPack>();
            this.RequisitionRepository = Substitute.For<IRepository<RequisitionHeader, int>>();

            this.PartNumber = "part 1";
            this.Quantity = 2;
            this.UserNumber = 808;
            this.ReqNumber = 909707;
            this.FromStockDate = 1.April(2025);
            this.Req = new RequisitionHeader
                           {
                               ReqNumber = this.ReqNumber,
                               Lines = new List<RequisitionLine>
                                           {
                                               new RequisitionLine
                                                   {
                                                       LineNumber = 1,
                                                       ReqNumber = this.ReqNumber,
                                                       Moves = new List<ReqMove>
                                                                   {
                                                                       new ReqMove
                                                                           {
                                                                               LineNumber = 1,
                                                                               ReqNumber = this.ReqNumber,
                                                                               Sequence = 1
                                                                           },
                                                                       new ReqMove
                                                                           {
                                                                               LineNumber = 1,
                                                                               ReqNumber = this.ReqNumber,
                                                                               Sequence = 2
                                                                           }
                                                                   }
                                                   },
                                               new RequisitionLine
                                                   {
                                                       LineNumber = 2,
                                                       ReqNumber = this.ReqNumber,
                                                       Moves = new List<ReqMove>
                                                                   {
                                                                       new ReqMove
                                                                           {
                                                                               LineNumber = 2,
                                                                               ReqNumber = this.ReqNumber,
                                                                               Sequence = 1
                                                                           },
                                                                       new ReqMove
                                                                           {
                                                                               LineNumber = 2,
                                                                               ReqNumber = this.ReqNumber,
                                                                               Sequence = 2
                                                                           }
                                                                   }
                                                   }
                                           }
                           };
            this.RequisitionRepository.FindById(this.ReqNumber).Returns(this.Req);
            this.Sut = new MoveStockService(this.StoresPack, this.RequisitionRepository);
        }
    }
}
