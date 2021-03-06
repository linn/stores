﻿namespace Linn.Stores.Facade.Tests.ParcelFacadeServiceTests
{
    using Linn.Common.Persistence;
    using Linn.Common.Proxy.LinnApps;
    using Linn.Stores.Domain.LinnApps;
    using Linn.Stores.Facade.Services;
    using NSubstitute;
    using NUnit.Framework;

    public class ContextBase
    {
        protected ParcelFacadeService Sut { get; private set; }

        protected IDatabaseService DatabaseService { get; private set; }

        protected IRepository<Parcel, int> ParcelRepository { get; private set; }

        protected ITransactionManager TransactionManager { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.ParcelRepository = Substitute.For<IRepository<Parcel, int>>();
            this.DatabaseService = Substitute.For<IDatabaseService>();
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.Sut = new ParcelFacadeService(
                this.ParcelRepository,
                this.TransactionManager,
                this.DatabaseService);
        }
    }
}
