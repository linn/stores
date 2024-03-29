﻿namespace Linn.Stores.Domain.LinnApps.Tests.TpkServiceTests
{
    using System;
    using System.Linq.Expressions;

    using Linn.Common.Logging;
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Consignments;
    using Linn.Stores.Domain.LinnApps.ExternalServices;
    using Linn.Stores.Domain.LinnApps.Requisitions;
    using Linn.Stores.Domain.LinnApps.StockLocators;
    using Linn.Stores.Domain.LinnApps.Tpk;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected ITpkService Sut { get; set; }

        protected IQueryRepository<TransferableStock> TpkView { get; set; }

        protected IQueryRepository<AccountingCompany> AccountingCompaniesRepository { get; set; }

        protected ITpkPack TpkPack { get; set; }

        protected IBundleLabelPack BundleLabelPack { get; set; }

        protected IWhatToWandService WhatToWandService { get; set; }

        protected IStoresPack StoresPack { get; set; }

        protected IQueryRepository<SalesAccount> SalesAccountRepository { get; set; }

        protected IRepository<Consignment, int> ConsignmentRepository { get; set; }

        protected IQueryRepository<SalesOrderDetail> SalesOrderDetailRepository { get; set; }

        protected IQueryRepository<SalesOrder> SalesOrderRepository { get; set; }

        protected IRepository<ReqMove, ReqMoveKey> ReqMovesRepository { get; set; }

        protected IQueryRepository<ProductUpgradeRule> ProductUpgradeRuleRepository { get; set; }

        protected ILog Logger { get; set; }

        protected IProductUpgradePack ProductUpgradePack { get; set; }

        [SetUp]
        public void SetUpContext()
        {
            this.ProductUpgradeRuleRepository = Substitute.For<IQueryRepository<ProductUpgradeRule>>();
            this.TpkView = Substitute.For<IQueryRepository<TransferableStock>>();
            this.AccountingCompaniesRepository = Substitute.For<IQueryRepository<AccountingCompany>>();
            this.TpkPack = Substitute.For<ITpkPack>();
            this.ProductUpgradePack = Substitute.For<IProductUpgradePack>();
            this.BundleLabelPack = Substitute.For<IBundleLabelPack>();
            this.WhatToWandService = Substitute.For<IWhatToWandService>();
            this.StoresPack = Substitute.For<IStoresPack>();
            this.SalesAccountRepository = Substitute.For<IQueryRepository<SalesAccount>>();
            this.ConsignmentRepository = Substitute.For<IRepository<Consignment, int>>();
            this.SalesOrderRepository = Substitute.For<IQueryRepository<SalesOrder>>();
            this.SalesOrderDetailRepository = Substitute.For<IQueryRepository<SalesOrderDetail>>();
            this.SalesOrderRepository.FindBy(Arg.Any<Expression<Func<SalesOrder, bool>>>())
                .Returns(new SalesOrder { OrderNumber = 1, CurrencyCode = "USD" });
            this.SalesOrderDetailRepository.FindBy(Arg.Any<Expression<Func<SalesOrderDetail, bool>>>())
                .Returns(new SalesOrderDetail() { OrderNumber = 1, OrderLine = 1, NettTotal = 100m });
            this.ConsignmentRepository.FindById(1)
                .Returns(new Consignment
                             {
                                 ConsignmentId = 1
                             });
            this.ConsignmentRepository.FindById(2)
                .Returns(new Consignment
                             {
                                 ConsignmentId = 2
                             });
            this.ConsignmentRepository.FindById(3)
                .Returns(new Consignment
                             {
                                 ConsignmentId = 3
                             });
            this.ReqMovesRepository = Substitute.For<IRepository<ReqMove, ReqMoveKey>>();
            this.Logger = Substitute.For<ILog>();
            this.Sut = new TpkService(
                this.TpkView,
                this.AccountingCompaniesRepository,
                this.TpkPack,
                this.BundleLabelPack,
                this.WhatToWandService,
                this.SalesAccountRepository,
                this.StoresPack,
                this.ConsignmentRepository,
                this.SalesOrderDetailRepository,
                this.SalesOrderRepository,
                this.ReqMovesRepository,
                this.ProductUpgradeRuleRepository,
                this.Logger,
                this.ProductUpgradePack);
        }
    }
}
