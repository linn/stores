﻿namespace Linn.Stores.Facade.Tests.WandFacadeServiceTests
{
    using Linn.Common.Persistence;
    using Linn.Stores.Domain.LinnApps.Wand;
    using Linn.Stores.Domain.LinnApps.Wand.Models;
    using Linn.Stores.Facade.Services;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected WandFacadeService Sut { get; private set; }

        protected IQueryRepository<WandConsignment> WandConsignmentsRepository { get; private set; }

        protected IQueryRepository<WandItem> WandItemsRepository { get; private set; }

        protected IWandService WandService { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.WandService = Substitute.For<IWandService>();
            this.WandConsignmentsRepository = Substitute.For<IQueryRepository<WandConsignment>>();
            this.WandItemsRepository = Substitute.For<IQueryRepository<WandItem>>();
            this.Sut = new WandFacadeService(
                this.WandConsignmentsRepository,
                this.WandItemsRepository,
                this.WandService);
        }
    }
}
